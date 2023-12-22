using QLStats.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace QLStats.LocalStorageParser.Models;

[Mapper]
public static partial class MatchJsonMapper
{
    public static void Map(MatchJson value, Match match)
    {
        PartialMap(value, match);
        match.PlayedAt = DateTimeOffset.FromUnixTimeMilliseconds(value.TimePlayed).UtcDateTime;
        match.TeamRedScore = value.Tscore0;
        match.TeamBlueScore = value.Tscore1;
    }

    private static partial void PartialMap(MatchJson value, Match match);
    private static bool IntToBool(int value) => value == 1;
}
