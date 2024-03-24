namespace ScoreBoard.Models;

public class Match
{
    public Team HomeTeam { get; set; }
    public Team AwayTeam { get; set; }
    public MatchStatuses MatchStatus { get; set; } = MatchStatuses.InProcess;
}