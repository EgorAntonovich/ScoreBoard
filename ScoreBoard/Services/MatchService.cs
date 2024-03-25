using ScoreBoard.Models;
using ScoreBoard.Services.Interfaces;

namespace ScoreBoard.Services;

public class MatchService : IMatchService
{
    public List<Match> ScoreBoard = new List<Match>();
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

        Match newMatch = new Match()
        {
            HomeTeam = homeTeam,
            AwayTeam = awayTeam,
        };
        
        ScoreBoard.Add(newMatch);

        return newMatch;
    }
}