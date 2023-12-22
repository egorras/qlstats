using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QLStats.Infrastructure.Data;

namespace QLStats.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        Guard.IsNotNullOrEmpty(connectionString);

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseSqlServer(connectionString, builder
                => builder.EnableRetryOnFailure());
        });

        services.AddScoped<ApplicationDbContextInitialiser>();

        return services;
    }
}
