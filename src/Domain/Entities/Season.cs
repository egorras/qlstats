namespace QLStats.Domain.Entities;

public class Season : BaseEntity
{
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }

    public override string ToString()
    {
        return $"Season #{Id} ({StartsAt:yyyy.MM.dd} - {EndsAt:yyyy.MM.dd})";
    }
}
