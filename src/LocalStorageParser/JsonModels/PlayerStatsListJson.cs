namespace QLStats.LocalStorageParser.Models;

public record PlayerStatsListJson
{
    public List<PlayerStatsJson> PlyrStats { get; set; } = [];
}
