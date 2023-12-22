namespace QLStats.LocalStorageParser.Models;

public record MatchJson
{
    public bool Aborted { get; set; }
    public int CaptureLimit { get; set; }
    public string ExitMsg { get; set; } = default!;
    public string Factory { get; set; } = default!;
    public string FactoryTitle { get; set; } = default!;
    public string FirstScorer { get; set; } = default!;
    public int FragLimit { get; set; }
    public int GameLength { get; set; }
    public string GameType { get; set; } = default!;
    public int Infected { get; set; }
    public int Instagib { get; set; }
    public int LastLeadChangeTime { get; set; }
    public string LastScorer { get; set; } = default!;
    public string LastTeamscorer { get; set; } = default!;
    public string Map { get; set; } = default!;
    public Guid MatchGuid { get; set; }
    public int MercyLimit { get; set; }
    public int Quadhog { get; set; }
    public int Restarted { get; set; }
    public int RoundLimit { get; set; }
    public int ScoreLimit { get; set; }
    public string ServerTitle { get; set; } = default!;
    public int TimeLimit { get; set; }
    public int Training { get; set; }
    public int Tscore0 { get; set; }
    public int Tscore1 { get; set; }
    public long TimePlayed { get; set; }
}
