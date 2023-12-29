namespace QLStats.Domain.Entities;

public class Season : BaseEntity
{
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }

    public int PtsForClanArenaRoundWin { get; set; } = 1;
    public int PtsForClanArenaMatchWin { get; set; } = 3;
    public bool UseGameScore { get; set; } = false;
    public decimal? DamageForOnePts { get; set; } = 100;

    public override string ToString()
    {
        return $"Season #{Id} ({StartsAt:yyyy.MM.dd} - {EndsAt:yyyy.MM.dd})";
    }
}
