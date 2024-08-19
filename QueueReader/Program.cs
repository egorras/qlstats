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

string serverAddress = "tcp://129.152.31.233:27960";
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
                        Map = data.MAP
                    };
                    context.Add(match);

                    suicideSteamId.Clear();
                }
                break;

            case "PLAYER_DEATH":
                {
                    var data = JsonSerializer.Deserialize<EventData<PlayerDeathEventData>>(message)!.DATA;
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

                    var suicideTimeout = 13;

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
                    playerStats.DamageDealt = data.Damage.Dealt;
                    playerStats.DamageTaken = data.Damage.Taken;

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
}

public class PlayerEventData
{
    public string STEAM_ID { get; set; } = string.Empty;
}

public class RoundOverEventData : MatchEventData
{
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
    public DamageEventData Damage { get; set; } = new();
}

public class DamageEventData
{
    public int Dealt { get; set; }
    public int Taken { get; set; }
}

public class MatchStartedEventData
{
    public string FACTORY { get; set; } = default!;
    public string FACTORY_TITLE { get; set; } = default!;
    public string GAME_TYPE { get; set; } = default!;
    public string MAP { get; set; } = default!;
    public Guid MATCH_GUID { get; set; } = default!;
}
