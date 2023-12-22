namespace QLStats.Domain.Entities;

public class Match : BaseEntity
{
    public Guid MatchGuid { get; set; }
    public string Map { get; set; } = default!;

    public DateTime PlayedAt { get; set; }
    public int? TeamBlueScore { get; set; }
    public int? TeamRedScore { get; set; }

    public List<MatchPlayerStats> PlayerStats { get; set; } = null!;
}
