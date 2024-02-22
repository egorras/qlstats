using System.Reflection;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using QLStats.Domain.Entities;

namespace QLStats.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Match> Matches => Set<Match>();
    public DbSet<MatchPlayerStats> MatchPlayerStats => Set<MatchPlayerStats>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Season> Seasons => Set<Season>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("QLSTATS");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
