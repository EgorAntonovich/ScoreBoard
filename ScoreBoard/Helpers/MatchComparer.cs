using ScoreBoard.Models;

namespace ScoreBoard.Helpers;

public class MatchComparer : IComparer<Match>
{
    public int Compare(Match? x, Match? y)
    {
        var xTeamTotalScore = x.AwayTeam.TeamScore + x.HomeTeam.TeamScore;
        var yTeamTotalScore = y.AwayTeam.TeamScore + y.HomeTeam.TeamScore;
        
        return xTeamTotalScore == yTeamTotalScore ? x.MatchSequenceIndex.CompareTo(y.MatchSequenceIndex) : xTeamTotalScore.CompareTo(yTeamTotalScore);
    }
}