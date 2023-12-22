namespace QLStats.Domain.Entities;

public class Season : BaseEntity
{
    public DateTime StartsAt { get; set; }
    public DateTime? EndsAt { get; set; }
}
