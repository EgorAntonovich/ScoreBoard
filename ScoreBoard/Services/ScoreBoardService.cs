using System.Reflection.Metadata;
using Microsoft.Extensions.Logging;
using ScoreBoard.Helpers;
using ScoreBoard.Models;
using ScoreBoard.Services.Interfaces;

namespace ScoreBoard.Services;

public class ScoreBoardService : IScoreBoardService
{
    public readonly List<Match> ScoreBoard = new List<Match>();
    
    public ProcessResult InitMatch(Team homeTeam, Team awayTeam)
    {
        if (homeTeam.TeamName == awayTeam.TeamName)
        {
            return new ProcessResult(false, "Two teams can't have the same names.", null);
        }

        if (homeTeam.TeamSide == awayTeam.TeamSide)
        {
            return new ProcessResult(false, "Two teams can't have the same sides.", null);
        }

        if (homeTeam.TeamSide != TeamSides.HomeTeam || awayTeam.TeamSide != TeamSides.AwayTeam)
        {
            return new ProcessResult(false, "Invalid teams side. First argument should be home team and second - away team.", null);
        }

        Match newMatch = new Match()
        {
            HomeTeam = homeTeam,
            AwayTeam = awayTeam,
        };
        
        ScoreBoard.Add(newMatch);

        return new ProcessResult(true, "Match has been successfully created.", newMatch);
    }

    public ProcessResult UpdateMatch(Team homeTeam, Team awayTeam)
    {
        var matchForUpdateIndex = GetMatchIndexByTeamsName(homeTeam.TeamName, awayTeam.TeamName);
        
        if (matchForUpdateIndex == -1)
        {
            return new ProcessResult(false, "There is not such match.", null);
        }
        
        ScoreBoard[matchForUpdateIndex].HomeTeam.TeamScore = homeTeam.TeamScore;
        ScoreBoard[matchForUpdateIndex].AwayTeam.TeamScore = awayTeam.TeamScore;

        return new ProcessResult(true, "Match has been successfully updated.", ScoreBoard[matchForUpdateIndex]);
    }

    public ProcessResult FinishMatch(Team homeTeam, Team awayTeam)
    {
        var matchIndexForFinish = GetMatchIndexByTeamsName(homeTeam.TeamName, awayTeam.TeamName);

        if (matchIndexForFinish == -1)
        {
            return new ProcessResult(false, "There is not such match for finish.", null);
        }

        ScoreBoard[matchIndexForFinish].MatchStatus = MatchStatuses.Completed;

        return new ProcessResult(true, "Match has been successfully finished.", ScoreBoard[matchIndexForFinish]);
    }

    public List<Match> GetSummaryOfMatches()
    {
        if (ScoreBoard.All(x => x.MatchStatus == MatchStatuses.Completed) || ScoreBoard.Count == 0)
        {
            throw new Exception("There are no any active matches.");
        }
        
        var activeMatches = ScoreBoard.Where(x => x.MatchStatus == MatchStatuses.InProcess).ToList();
        activeMatches.Sort(new MatchComparer());
        activeMatches.Reverse();
        
        return activeMatches;
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