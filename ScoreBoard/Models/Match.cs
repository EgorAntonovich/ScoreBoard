using ScoreBoard.Models.Enums;

namespace ScoreBoard.Models;

public class Match
{
    private static int _count = 0;

    public Match()
    {
        this.MatchSequenceIndex = _count++;
    }
    public Team HomeTeam { get; set; }
    public Team AwayTeam { get; set; }
    public MatchStatuses MatchStatus { get; set; } = MatchStatuses.InProcess;
    public int MatchSequenceIndex { get; }
}