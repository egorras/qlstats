namespace QLStats.Domain.Entities;

public class Season : BaseEntity
{
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PtsForClanArenaRoundWin { get; set; } = 1;
    public int PtsForClanArenaMatchWin { get; set; } = 3;
    public bool UseGameScore { get; set; } = false;
    public decimal? DamageForOnePts { get; set; } = 100;
    public int PtsPerMedal { get; set; } = 0;

    public override string ToString()
    {
        return $"Season #{Id} {Name} ({StartsAt:yyyy.MM.dd} - {EndsAt:yyyy.MM.dd})";
    }
}
