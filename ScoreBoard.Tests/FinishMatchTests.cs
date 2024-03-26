using FluentAssertions;
using ScoreBoard.Models;
using ScoreBoard.Services;

namespace ScoreBoard.Tests;

public class FinishMatchTests
{
    public static IEnumerable<object[]> GetValidDataGenerator()
    {
        yield return new object[] 
        {
            new Team()
            {
                TeamName = "Mexico",
                TeamSide = TeamSides.HomeTeam,
            },
            new Team()
            {
                TeamName = "Spain",
                TeamSide = TeamSides.AwayTeam,
            },
        };
        yield return new object[] 
        {
            new Team()
            {
                TeamName = "Uruguay",
                TeamSide = TeamSides.HomeTeam,
            },
            new Team()
            {
                TeamName = "Italy",
                TeamSide = TeamSides.AwayTeam,
            },
        };
    }
    
    [Theory]
    [MemberData(nameof(GetValidDataGenerator))]
    public void FinishMatch_FinishFailed_ReturnMatchNotExistsMessage(Team firstTeam, Team secondTeam)
    {
        // Arrange
        var scoreBoardService = new ScoreBoardService();
        var homeTeam = new Team()
        {
            TeamName = "Italy",
            TeamSide = TeamSides.HomeTeam,
        };
        var awayTeam = new Team()
        {
            TeamName = "Uruguay",
            TeamSide = TeamSides.AwayTeam,
        };

        // Act
        scoreBoardService.InitMatch(homeTeam, awayTeam);
        Action action = () => scoreBoardService.FinishMatch(firstTeam, secondTeam);
        
        // Assert
        action.Should().ThrowExactly<Exception>().WithMessage("There is not such match for finish.");
    }
    
    [Theory]
    [MemberData(nameof(GetValidDataGenerator))]
    public void FinishMatch_FinishSucceed_ReturnMatchWithCompletedStatus(Team firstTeam, Team secondTeam)
    {
        // Arrange
        var scoreBoardService = new ScoreBoardService();
        
        var firstHomeTeam = new Team()
        {
            TeamName = "Mexico",
            TeamSide = TeamSides.HomeTeam,
        };
        var firstAwayTeam = new Team()
        {
            TeamName = "Spain",
            TeamSide = TeamSides.AwayTeam,
        };

        var secondHomeTeam = new Team()
        {
            TeamName = "Uruguay",
            TeamSide = TeamSides.HomeTeam,
        };
        var secondAwayTeam = new Team()
        {
            TeamName = "Italy",
            TeamSide = TeamSides.AwayTeam,
        };
        
        // Act
        scoreBoardService.InitMatch(firstHomeTeam, firstAwayTeam);
        scoreBoardService.InitMatch(secondHomeTeam, secondAwayTeam);
        var finishedMatch = scoreBoardService.FinishMatch(firstTeam, secondTeam);
        
        // Assert
        finishedMatch.MatchStatus.Should().Be(MatchStatuses.Completed);
    }
}