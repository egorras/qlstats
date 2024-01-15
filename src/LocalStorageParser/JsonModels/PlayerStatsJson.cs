using System.Text.Json.Serialization;

namespace QLStats.LocalStorageParser.Models;

public record PlayerStatsJson
{
    public string Name { get; set; } = default!;
    public string SteamId { get; set; } = default!;
    public Guid MatchGuid { get; set; }
    public int Score { get; set; }
    public int? Team { get; set; }
    public int Win { get; set; }
    public DamageJson Damage { get; set; } = new();
    public MedalsJson Medals { get; set; } = new();
}

public record DamageJson
{
    public int Dealt { get; set; }
    public int Taken { get; set; }
}

public record MedalsJson
{
    public int Accuracy { get; set; }
    public int Excellent { get; set; }
    public int Firstfrag { get; set; }
    public int Headshot { get; set; }
    public int Humiliation { get; set; }
    public int Impressive { get; set; }
    public int Midair { get; set; }
    public int Perfect { get; set; }
    public int Rampage { get; set; }
    public int Revenge { get; set; }

    [JsonIgnore]
    public int Total =>
        Accuracy
        + Excellent
        + Firstfrag
        + Headshot
        + Humiliation
        + Impressive
        + Midair
        + Perfect
        + Rampage
        + Revenge;
}
