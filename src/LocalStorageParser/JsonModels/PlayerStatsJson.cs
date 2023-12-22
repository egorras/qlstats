namespace QLStats.LocalStorageParser.Models;

public record PlayerStatsJson
{
    public string Name { get; set; } = default!;
    public string SteamId { get; set; } = default!;
    public Guid MatchGuid { get; set; }
    public int Score { get; set; }
    public int? Team { get; set; }
    public int Win { get; set; }
}
