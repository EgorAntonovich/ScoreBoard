using ScoreBoard.Models;
using ScoreBoard.Services.Interfaces;

namespace ScoreBoard.Services;

public class ScoreBoardService : IScoreBoardService
{
    public readonly List<Match> ScoreBoard = new List<Match>();
    public Match InitMatch(Team homeTeam, Team awayTeam)
    {
        if (homeTeam.TeamName == awayTeam.TeamName)
        {
            throw new ArgumentException("Two teams can't have the same names.");
        }

        if (homeTeam.TeamSide == awayTeam.TeamSide)
        {
            throw new ArgumentException("Two teams can't have the same sides.");
        }

        if (homeTeam.TeamSide != TeamSides.HomeTeam || awayTeam.TeamSide != TeamSides.AwayTeam)
        {
            throw new ArgumentException(
                "Invalid teams side. First argument should be home team and second - away team");
        }

        Match newMatch = new Match()
        {
            HomeTeam = homeTeam,
            AwayTeam = awayTeam,
        };
        
        ScoreBoard.Add(newMatch);

        return newMatch;
    }

    public Match UpdateMatch(Team homeTeam, Team awayTeam)
    {
        var matchForUpdateIndex = GetMatchIndexByTeamsName(homeTeam.TeamName, awayTeam.TeamName);
        
        if (matchForUpdateIndex == -1)
        {
            throw new Exception("There is not such match.");
        }
        
        ScoreBoard[matchForUpdateIndex].HomeTeam.TeamScore = homeTeam.TeamScore;
        ScoreBoard[matchForUpdateIndex].AwayTeam.TeamScore = awayTeam.TeamScore;

        return ScoreBoard[matchForUpdateIndex];
    }

    public Match FinishMatch(Team homeTeam, Team awayTeam)
    {
        var matchIndexForFinish = GetMatchIndexByTeamsName(homeTeam.TeamName, awayTeam.TeamName);

        if (matchIndexForFinish == -1)
        {
            throw new Exception("There is not such match for finish.");
        }

        ScoreBoard[matchIndexForFinish].MatchStatus = MatchStatuses.Completed;

        return ScoreBoard[matchIndexForFinish];
    }

    private int GetMatchIndexByTeamsName(string homeTeamName, string awayTeamName)
    {
        return ScoreBoard
            .FindIndex(x => 
                x.AwayTeam.TeamName == awayTeamName &&
                x.HomeTeam.TeamName == homeTeamName &&
                x.AwayTeam.TeamSide == TeamSides.AwayTeam &&
                x.HomeTeam.TeamSide == TeamSides.HomeTeam);
    }
}