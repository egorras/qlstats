namespace QLStats.Domain.Entities;

public class Player : BaseEntity
{
    public string Name { get; set; } = default!;
    public long SteamId { get; set; }
}
