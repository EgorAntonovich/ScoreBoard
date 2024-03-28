using System.Collections;
using ScoreBoard.Models;

namespace ScoreBoard.Tests.Helpers;

public class MatchEqualityComparer : IEqualityComparer<Match>
{
    public bool Equals(Match? x, Match? y)
    {
        if (ReferenceEquals(x, y)) return true;

        if (x == null || y == null) return false;

        return x.HomeTeam.TeamName == y.HomeTeam.TeamName
               && x.HomeTeam.TeamScore == y.HomeTeam.TeamScore
               && x.HomeTeam.TeamSide == y.HomeTeam.TeamSide
               && x.AwayTeam.TeamName == y.AwayTeam.TeamName
               && x.AwayTeam.TeamScore == y.AwayTeam.TeamScore
               && x.AwayTeam.TeamSide == y.AwayTeam.TeamSide
               && x.MatchStatus == y.MatchStatus;
    }

    public int GetHashCode(Match? obj)
    {
        if (obj == null) return 0;

        return HashCode.Combine(
            obj.HomeTeam.TeamName,
            obj.HomeTeam.TeamScore,
            obj.HomeTeam.TeamSide,
            obj.AwayTeam.TeamName,
            obj.AwayTeam.TeamScore,
            obj.AwayTeam.TeamSide,
            obj.MatchStatus);
    }
}