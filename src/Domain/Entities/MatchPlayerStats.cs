namespace QLStats.Domain.Entities;

public class MatchPlayerStats : BaseEntity
{
    public int MatchId { get; set; }
    public virtual Match Match { get; set; } = null!;

    public int PlayerId { get; set; }
    public virtual Player Player { get; set; } = null!;

    public int Score { get; set; }
    public int Kills { get; set; }
    public bool Win { get; set; }
    public Team? Team { get; set; }
    public int? TeamScore { get; set; }
    public int DamageDealt { get; set; }
    public int DamageTaken { get; set; }
    public int MedalsTotal { get; set; }

    public decimal CalculatePtsFor(Season season)
    {
        var pts = decimal.Zero;

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
        }

        if (season.UseGameScore)
        {
            pts += Score;
        }

        if (season.DamageForOnePts > 0)
        {
            pts += Math.Floor(DamageDealt / season.DamageForOnePts.Value);
        }

        if (season.PtsPerMedal > 0)
        {
            pts += MedalsTotal * season.PtsPerMedal;
        }

        if (season.PtsPerKill > 0)
        {
            pts += Kills * season.PtsPerKill;
        }

        return pts;
    }
}
