using FluentAssertions;
using ScoreBoard.Models;
using ScoreBoard.Models.Enums;
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
        var processResult =  scoreBoardService.FinishMatch(firstTeam, secondTeam);
        
        // Assert
        processResult.Data.Should().Be(null);
        processResult.Flag.Should().Be(false);
        processResult.Message.Should().Be("There is not such match for finish.");
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
        var processResult = scoreBoardService.FinishMatch(firstTeam, secondTeam);
        
        // Assert
        processResult.Data.MatchStatus.Should().Be(MatchStatuses.Completed);
        processResult.Flag.Should().Be(true);
        processResult.Message.Should().Be("Match has been successfully finished.");
    }
}