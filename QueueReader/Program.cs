using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetMQ;
using NetMQ.Sockets;
using QLStats.Domain.Entities;
using QLStats.Domain.Enums;
using QLStats.Infrastructure.Data;

Console.WriteLine("Hello, World!");

var serviceProvider = new ServiceCollection()
    .AddDbContext<ApplicationDbContext>(x =>
        x.UseNpgsql("Host=ep-empty-mountain-a2dlf6xq.eu-central-1.aws.neon.tech;Database=qlstatsdb;Username=webapp;Password=p5P2seuIncLB;SslMode=Require", x => x
            .EnableRetryOnFailure()
            .MigrationsHistoryTable("__EFMigrationsHistory", "QLSTATS")))
    .BuildServiceProvider();

using var scope = serviceProvider.CreateScope();

var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

var suicideSteamId = new Dictionary<long, int>();

string serverAddress = "tcp://213.133.99.206:26544";
using (var subscriber = new SubscriberSocket())
{
    Console.WriteLine("Connecting to Quake Live server as a subscriber...");
    subscriber.Connect(serverAddress);
    subscriber.Subscribe("");

    while (true)
    {
        // Receive updates from the server
        string message = subscriber.ReceiveFrameString();
        Console.WriteLine($"{message}");

        var eventType = JsonSerializer.Deserialize<EventType>(message);
        switch (eventType!.TYPE)
        {
            case "MATCH_STARTED":
                {
                    var data = JsonSerializer.Deserialize<EventData<MatchStartedEventData>>(message)!.DATA;
                    var match = new Match
                    {
                        ExitMsg = string.Empty,
                        CaptureLimit = 0,
                        Aborted = false,
                        Factory = data.FACTORY,
                        FactoryTitle = data.FACTORY_TITLE,
                        FirstScorer = string.Empty,
                        LastScorer = string.Empty,
                        FragLimit = 0,
                        GameLength = 0,
                        GameType = data.GAME_TYPE,
                        PlayedAt = DateTime.UtcNow,
                        Instagib = false,
                        MatchGuid = data.MATCH_GUID,
                        ServerTitle = string.Empty,
                        LastTeamscorer = string.Empty,
                        Map = data.MAP,
                        PlayerStats = []
                    };
                    context.Add(match);

                    foreach (var playerData in data.PLAYERS)
                    {
                        var steamId = long.Parse(playerData.STEAM_ID);
                        var player = await context.Players.FirstAsync(x => x.SteamId == steamId);
                        match.PlayerStats.Add(new MatchPlayerStats
                        {
                            Player = player,
                            Team = playerData.TEAM == 1 ? Team.Red : Team.Blue,
                            TeamScore = 0
                        });
                    }

                    suicideSteamId.Clear();
                }
                break;

            case "PLAYER_DEATH":
                {
                    var data = JsonSerializer.Deserialize<EventData<PlayerDeathEventData>>(message)!.DATA;
                    if (data.WARMUP)
                    {
                        continue;
                    }

                    var victimSteamId = long.Parse(data.VICTIM.STEAM_ID);
                    if (data.KILLER is null)
                    {
                        suicideSteamId[victimSteamId] = data.TIME;
                    }
                    else
                    {
                        suicideSteamId.Remove(victimSteamId);
                    }
                }
                break;

            case "ROUND_OVER":
                {
                    var data = JsonSerializer.Deserialize<EventData<RoundOverEventData>>(message)!.DATA;

                    var match = await context.Matches.Include(x => x.PlayerStats).FirstAsync(x => x.MatchGuid == data.MATCH_GUID);
                    if (match is null)
                    {
                        continue;
                    }

                    Team? teamWon = data.TEAM_WON == "RED"
                        ? Team.Red
                        : data.TEAM_WON == "BLUE"
                        ? Team.Blue
                        : null;

                    if (teamWon is not null)
                    {
                        foreach (var playerStat in match.PlayerStats.Where(x => x.Team == teamWon))
                        {
                            playerStat.TeamScore += 1;
                        }

                        if (teamWon == Team.Red)
                        {
                            match.TeamRedScore += 1;
                        }
                        else
                        {
                            match.TeamBlueScore += 1;
                        }
                    }

                    var suicideTimeout = 11;

                    foreach (var steamId in suicideSteamId.Where(x => data.TIME - suicideTimeout < x.Value))
                    {
                        var player = await context.Players.FirstAsync(x => x.SteamId == steamId.Key);
                        var playerStats = match.PlayerStats.FirstOrDefault(x => x.PlayerId == player.Id);
                        if (playerStats is null)
                        {
                            playerStats = new MatchPlayerStats { Player = player };
                            match.PlayerStats.Add(playerStats);
                        }
                        playerStats.Suicides += 1;
                    }

                    suicideSteamId.Clear();
                }
                break;
            case "PLAYER_STATS":
                {
                    var data = JsonSerializer.Deserialize<EventData<PlayerStatsEventData>>(message)!.DATA;
                    var match = await context.Matches.Include(x => x.PlayerStats).FirstOrDefaultAsync(x => x.MatchGuid == data.MATCH_GUID);
                    if (match is null)
                    {
                        continue;
                    }

                    var steamId = long.Parse(data.STEAM_ID);
                    var player = await context.Players.FirstAsync(x => x.SteamId == steamId);

                    var playerStats = match.PlayerStats.FirstOrDefault(x => x.PlayerId == player.Id);
                    if (playerStats is null)
                    {
                        playerStats = new() { Player = player };
                        match.PlayerStats.Add(playerStats);
                    }

                    playerStats.Kills = data.KILLS;
                    playerStats.Win = data.WIN == 1;
                    playerStats.Score = data.SCORE;
                    playerStats.DamageDealt = data.DAMAGE.DEALT;
                    playerStats.DamageTaken = data.DAMAGE.TAKEN;
                    playerStats.MedalsTotal = data.MEDALS.PERFECT;

                    if (data.TEAM != null)
                    {
                        playerStats.Team = data.TEAM == 1 ? Team.Red : Team.Blue;
                        playerStats.TeamScore = playerStats.Team == Team.Red
                            ? match.TeamRedScore
                            : match.TeamBlueScore;
                    }
                    else
                    {
                        playerStats.Team = null;
                        playerStats.TeamScore = null;
                    }

                    suicideSteamId.Clear();
                }
                break;

            case "MATCH_REPORT":
                {
                    var data = JsonSerializer.Deserialize<EventData<MatchReportEventData>>(message)!.DATA;
                    var match = await context.Matches.Include(x => x.PlayerStats).FirstOrDefaultAsync(x => x.MatchGuid == data.MATCH_GUID);
                    if (match is null)
                    {
                        continue;
                    }

                    match.TeamRedScore = data.TSCORE0;
                    match.TeamBlueScore = data.TSCORE1;

                    foreach (var item in match.PlayerStats)
                    {
                        item.TeamScore = item.Team == Team.Red ? data.TSCORE0 : data.TSCORE1;
                    }
                }
                break;
        }

        await context.SaveChangesAsync();
    }
}

public class EventType
{
    public string TYPE { get; set; } = string.Empty;
}

public class EventData<T>
{
    public T DATA { get; set; } = default!;
}

public class PlayerDeathEventData : MatchEventData
{
    public PlayerEventData VICTIM { get; set; } = new();
    public PlayerEventData? KILLER { get; set; }
    public bool WARMUP { get; set; }
}

public class PlayerEventData
{
    public string STEAM_ID { get; set; } = string.Empty;
}

public class RoundOverEventData : MatchEventData
{
    public string TEAM_WON { get; set; } = string.Empty;
}

public class MatchEventData
{
    public Guid MATCH_GUID { get; set; }
    public int TIME { get; set; }
}

public class PlayerStatsEventData
{
    public string NAME { get; set; } = default!;
    public string STEAM_ID { get; set; } = default!;
    public Guid MATCH_GUID { get; set; }
    public int SCORE { get; set; }
    public int? TEAM { get; set; }
    public int WIN { get; set; }
    public int KILLS { get; set; }
    public DamageEventData DAMAGE { get; set; } = new();
    public MedalsEventData MEDALS { get; set; } = new();
}

public class DamageEventData
{
    public int DEALT { get; set; }
    public int TAKEN { get; set; }
}

public class MedalsEventData
{
    public int PERFECT { get; set; }
}


public class MatchStartedEventData
{
    public string FACTORY { get; set; } = default!;
    public string FACTORY_TITLE { get; set; } = default!;
    public string GAME_TYPE { get; set; } = default!;
    public string MAP { get; set; } = default!;
    public Guid MATCH_GUID { get; set; } = default!;
    public List<PlayersData> PLAYERS { get; set; } = [];

    public class PlayersData
    {
        public string STEAM_ID { get; set; } = default!;
        public int TEAM { get; set; }
    }
}

public class MatchReportEventData
{
    public Guid MATCH_GUID { get; set; }
    public int TSCORE0 { get; set; }
    public int TSCORE1 { get; set; }
}
