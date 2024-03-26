using FluentAssertions;
using ScoreBoard.Models;
using ScoreBoard.Services;

namespace ScoreBoard.Tests;

public class UpdateMatchTests
{
    public static IEnumerable<object[]> GetTeamsDataGenerator()
    {
        yield return new object[] 
        {
            new Team()
            {
                TeamName = "Mexico",
                TeamSide = TeamSides.HomeTeam,
                TeamScore = 4
            },
            new Team()
            {
                TeamName = "Spain",
                TeamSide = TeamSides.AwayTeam,
                TeamScore = 2
            },
        };
        yield return new object[] 
        {
            new Team()
            {
                TeamName = "Uruguay",
                TeamSide = TeamSides.HomeTeam,
                TeamScore = 6
            },
            new Team()
            {
                TeamName = "Italy",
                TeamSide = TeamSides.AwayTeam,
                TeamScore = 6
            },
        };
    }
    
    [Theory]
    [MemberData(nameof(GetTeamsDataGenerator))]
    public void UpdateMatch_UpdateFailed_ReturnsArgumentExceptionInvalidTeamsMessage(Team firstTeam, Team secondTeam)
    {
        // Arrange
        var scoreBoardService = new ScoreBoardService();
        var homeTeam = new Team()
        {
            TeamName = "Mexico",
            TeamSide = TeamSides.HomeTeam,
        };
        var awayTeam = new Team()
        {
            TeamName = "Argentina",
            TeamSide = TeamSides.AwayTeam,
        };
        
        // Act
        scoreBoardService.InitMatch(homeTeam, awayTeam);
        Action action = () => scoreBoardService.UpdateMatch(firstTeam, secondTeam);

        // Assert
        action.Should().ThrowExactly<Exception>().WithMessage("There is not such match.");
    }

    [Theory]
    [MemberData(nameof(GetTeamsDataGenerator))]
    public void UpdateMatch_UpdatedSuccess_ReturnsUpdatedMatch(Team firstTeam, Team secondTeam)
    {
        // Arrange
        var scoreBoardService = new ScoreBoardService();
        Team firstHomeTeam = new Team()
        {
            TeamName = "Mexico",
            TeamSide = TeamSides.HomeTeam,
        };
        
        Team firstAwayTeam = new Team()
        {
            TeamName = "Spain",
            TeamSide = TeamSides.AwayTeam,
        };
        
        Team secondHomeTeam = new Team()
        {
            TeamName = "Uruguay",
            TeamSide = TeamSides.HomeTeam,
        };
        
        Team secondAwayTeam = new Team()
        {
            TeamName = "Italy",
            TeamSide = TeamSides.AwayTeam,
        };
        
        // Act
        scoreBoardService.InitMatch(firstHomeTeam, firstAwayTeam);
        scoreBoardService.InitMatch(secondHomeTeam, secondAwayTeam);
        var updatedMatch = scoreBoardService.UpdateMatch(firstTeam, secondTeam);
        
        // Assert
        updatedMatch.HomeTeam.TeamScore.Should().Be(firstTeam.TeamScore);
        updatedMatch.AwayTeam.TeamScore.Should().Be(secondTeam.TeamScore);
    }
}