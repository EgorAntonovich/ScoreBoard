using ScoreBoard.Models;

namespace ScoreBoard.Services.Interfaces;

public interface IScoreBoardService
{
    public Match InitMatch(Team homeTeam, Team awayTeam);
    public Match UpdateMatch(Team homeTeam, Team awayTeam);
    public Match FinishMatch(Team homeTeam, Team awayTeam);
    public List<Match> GetSummaryOfMatches();
}