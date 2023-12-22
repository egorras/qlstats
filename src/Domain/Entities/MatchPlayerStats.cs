namespace QLStats.Domain.Entities;

public class MatchPlayerStats : BaseEntity
{
    public int MatchId { get; set; }
    public virtual Match Match { get; set; } = null!;

    public int PlayerId { get; set; }
    public virtual Player Player { get; set; } = null!;

    public int Score { get; set; }
    public bool Win { get; set; }
    public Team? Team { get; set; }
    public int? TeamScore { get; set; }

    public int CalculatePtsFor(Season season)
    {
        var pts = 0;

        // ClanArena
        if (TeamScore != null)
        {
            if (season.PtsForClanArenaRoundWin > 0)
            {
                pts += TeamScore.Value * season.PtsForClanArenaRoundWin;
            }

            if (season.PtsForClanArenaMatchWin > 0 && Win)
            {
                pts += season.PtsForClanArenaMatchWin;
            }

            if (season.UseGameScore)
            {
                pts += Score;
            }
        }

        return pts;
    }
}
