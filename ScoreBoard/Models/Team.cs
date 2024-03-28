using ScoreBoard.Models.Enums;

namespace ScoreBoard.Models;

public class Team
{
    public string TeamName { get; set; }
    public TeamSides TeamSide { get; set; }
    public int TeamScore { get; set; } = 0;
}