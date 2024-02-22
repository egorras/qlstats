using Microsoft.EntityFrameworkCore;
using QLStats.Domain.Entities;
using QLStats.Domain.Enums;
using QLStats.Infrastructure.Data;
using QLStats.LocalStorageParser.Models;
using System.Text.Json;

internal class LocalStorageJsonParser
{
    private static readonly JsonSerializerOptions ReadOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseUpper
    };

    private readonly ApplicationDbContext _context;

    private HashSet<Guid> _parsedMatchGuids = [];

    public LocalStorageJsonParser(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task HandleJsonAsync(string key, string json) => key switch
    {
        "match_index" => Task.CompletedTask,
        var x when x.StartsWith("events_") => Task.CompletedTask,
        var x when x.StartsWith("match_") => HandleMatchJsonAsync(json),
        var x when x.StartsWith("players_") => HandlePlayersJsonAsync(json),
        _ => Task.CompletedTask
    };

    private async Task HandleMatchJsonAsync(string json)
    {
        var matchJson = JsonSerializer.Deserialize<MatchJson>(json, ReadOptions)!;
        if (_parsedMatchGuids.Contains(matchJson.MatchGuid))
        {
            return;
        }

        var match = await _context.Matches.FirstOrDefaultAsync(x => x.MatchGuid == matchJson.MatchGuid);
        if (match is null)
        {
            match = new Match { MatchGuid = matchJson.MatchGuid };
            _context.Add(match);
        }
        MatchJsonMapper.Map(matchJson, match);
        if (match.Id == 0)
        {
            await _context.SaveChangesAsync();
        }
    }

    private async Task HandlePlayersJsonAsync(string json)
    {
        var list = JsonSerializer.Deserialize<PlayerStatsListJson>(json, ReadOptions)!;
        if (list.PlyrStats.Count == 0)
        {
            return;
        }

        var matchGuid = list.PlyrStats[0].MatchGuid;
        if (_parsedMatchGuids.Contains(matchGuid))
        {
            return;
        }

        var match = await _context.Matches
            .Include(x => x.PlayerStats)
            .SingleAsync(x => x.MatchGuid == matchGuid);

        foreach (var playerStatsJson in list.PlyrStats)
        {
            var steamId = long.Parse(playerStatsJson.SteamId);
            var player = await _context.Players.FirstOrDefaultAsync(x => x.SteamId == steamId);
            if (player is null)
            {
                player = new Player() { Name = playerStatsJson.Name, SteamId = steamId };
                _context.Add(player);
                await _context.SaveChangesAsync();
            }

            var playerStats = match.PlayerStats.FirstOrDefault(x => x.PlayerId == player.Id);
            if (playerStats is null)
            {
                playerStats = new MatchPlayerStats { Player = player };
                match.PlayerStats.Add(playerStats);
            }

            playerStats.Win = playerStatsJson.Win == 1;
            playerStats.Score = playerStatsJson.Score;
            playerStats.DamageDealt = playerStatsJson.Damage.Dealt;
            playerStats.DamageTaken = playerStatsJson.Damage.Taken;
            playerStats.MedalsTotal = playerStatsJson.Medals.Total;

            if (playerStatsJson.Team != null)
            {
                playerStats.Team = playerStatsJson.Team == 1 ? Team.Red : Team.Blue;
                playerStats.TeamScore = playerStats.Team == Team.Red
                    ? match.TeamRedScore
                    : match.TeamBlueScore;
            }
            else
            {
                playerStats.Team = null;
                playerStats.TeamScore = null;
            }
        }
    }

    public async Task InitAsync()
    {
        await _context.Database.MigrateAsync();

        _parsedMatchGuids = (await _context.Matches.Select(x => x.MatchGuid).ToListAsync()).ToHashSet();
    }

    public Task FinishAsync() => _context.SaveChangesAsync();
}
