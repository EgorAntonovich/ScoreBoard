using ScoreBoard.Models;

namespace ScoreBoard.Helpers;

public class MatchComparer : IComparer<Match>
{
    public int Compare(Match firstMatch, Match secondMatch)
    {
        int firstMatchTotalScore = firstMatch.AwayTeam.TeamScore + firstMatch.HomeTeam.TeamScore;
        int secondMatchTotalScore = secondMatch.AwayTeam.TeamScore + secondMatch.HomeTeam.TeamScore;

        if (firstMatchTotalScore != secondMatchTotalScore)
        {
            return firstMatchTotalScore.CompareTo(secondMatchTotalScore);
        }

        return firstMatch.MatchStartDate.CompareTo(secondMatch.MatchStartDate);
    }
}