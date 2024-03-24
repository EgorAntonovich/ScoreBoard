using ScoreBoard.Models;

namespace ScoreBoard.Services.Interfaces;

public interface IMatchService
{
    public Match InitMatch(Team homeTeam, Team awayTeam);
}