using CommandLine;

namespace QLStats.LocalStorageParser;

internal class CommandLineOptions
{
    [Option("path", Required = true)]
    public required string LocalStorageFilePath { get; set; }

    [Option("cs", Required = true)]
    public required string ConnectionString { get; set; }
}