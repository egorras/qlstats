using System.Text;
using CommandLine;
using CommunityToolkit.Diagnostics;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QLStats.Infrastructure.Data;
using QLStats.LocalStorageParser;

await Parser.Default.ParseArguments<CommandLineOptions>(args)
    .WithParsedAsync(RunAsync);

static async Task RunAsync(CommandLineOptions options)
{
    Guard.IsTrue(
        File.Exists(options.LocalStorageFilePath),
        $"Local storage file not found in {options.LocalStorageFilePath}");

    var serviceProvider = new ServiceCollection()
        .AddScoped<LocalStorageJsonParser>()
        .AddDbContext<ApplicationDbContext>(x =>
            x.UseNpgsql(options.ConnectionString, x => x
                .EnableRetryOnFailure()
                .MigrationsHistoryTable("__EFMigrationsHistory", "QLSTATS")))
        .BuildServiceProvider();

    using var scope = serviceProvider.CreateScope();

    using var connection = new SqliteConnection(new SqliteConnectionStringBuilder
    {
        DataSource = options.LocalStorageFilePath
    }.ConnectionString);

    connection.Open();

    var command = new SqliteCommand(@"
SELECT key, value
FROM ItemTable
ORDER BY CASE
    WHEN key LIKE 'match_%' THEN 1
    WHEN key LIKE 'players_%' THEN 2
    ELSE 3
END;", connection);

    using var reader = await command.ExecuteReaderAsync();

    var localStorageParser = scope.ServiceProvider.GetRequiredService<LocalStorageJsonParser>();
    await localStorageParser.InitAsync();
    while (await reader.ReadAsync())
    {
        var key = (string)reader["key"];
        Console.WriteLine(key);
        var json = Encoding.Unicode.GetString((byte[])reader["value"]);
        await localStorageParser.HandleJsonAsync(key, json);
    }
    Console.WriteLine("Finished reading localstorage");
    await localStorageParser.FinishAsync();
}
