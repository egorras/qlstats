namespace QLStats.Domain.Entities;

public class MatchPlayerStats : BaseEntity
{
    public int MatchId { get; set; }
    public virtual Match Match { get; set; } = null!;

    public int PlayerId { get; set; }
    public virtual Player Player { get; set; } = null!;

    public bool Win { get; set; }
    public Team? Team { get; set; }
    public int? TeamScore { get; set; }
}
