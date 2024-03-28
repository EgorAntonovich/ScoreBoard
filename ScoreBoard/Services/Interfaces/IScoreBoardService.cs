using ScoreBoard.Models;

namespace ScoreBoard.Services.Interfaces;

public interface IScoreBoardService
{
    public ProcessResult InitMatch(Team homeTeam, Team awayTeam);
    public ProcessResult UpdateMatch(Team homeTeam, Team awayTeam);
    public ProcessResult FinishMatch(Team homeTeam, Team awayTeam);
    public List<Match> GetSummaryOfMatches();
}