using FluentAssertions;
using ScoreBoard.Models;
using ScoreBoard.Services;

namespace ScoreBoard.Tests;

public class StartNewMatchTests()
{
    public static IEnumerable<object[]> GetTeamsDataWithInvalidSidesGenerator()
    {
        yield return new object[] 
        {
            new Team()
            {
                TeamName = "Mexico",
                TeamSide = TeamSides.AwayTeam
            },
            new Team()
            {
                TeamName = "Canada",
                TeamSide = TeamSides.AwayTeam
            },
        };
        yield return new object[]
        {
            new Team()
            {
                TeamName = "Mexico",
                TeamSide = TeamSides.HomeTeam
            },
            new Team()
            {
                TeamName = "Canada",
                TeamSide = TeamSides.HomeTeam
            },
        };
    } 
    
    public static IEnumerable<object[]> GetTeamsDataWithSameNamesGenerator()
    {
        yield return new object[] 
        {
            new Team()
            {
                TeamName = "Mexico",
                TeamSide = TeamSides.AwayTeam
            },
            new Team()
            {
                TeamName = "Mexico",
                TeamSide = TeamSides.AwayTeam
            },
        };
    }
    
    public static IEnumerable<object[]> GetTeamsDataWithValidData()
    {
        yield return new object[] 
        {
            new Team()
            {
                TeamName = "Mexico",
                TeamSide = TeamSides.AwayTeam
            },
            new Team()
            {
                TeamName = "Canada",
                TeamSide = TeamSides.HomeTeam
            },
            1
        };
        yield return new object[]
        {
            new Team()
            {
                TeamName = "Spin",
                TeamSide = TeamSides.AwayTeam
            },
            new Team()
            {
                TeamName = "Brazil",
                TeamSide = TeamSides.HomeTeam
            },
            2
        };
    } 
    
    [Theory]
    [MemberData(nameof(GetTeamsDataWithInvalidSidesGenerator))]
    public void InitMatch_NotInitialized_ReturnsNotTwoSameTeamSidesErrorMessage(Team firstTeam, Team secondTeam)
    {
        //Arrange
        var matchService = new MatchService();
        
        // Act
        Action action = () => matchService.InitMatch(firstTeam, secondTeam);
        
        //Assert
        action.Should().ThrowExactly<ArgumentException>().WithMessage("Two teams can't have the same sides.");
    }
    
    [Theory]
    [MemberData(nameof(GetTeamsDataWithSameNamesGenerator))]
    public void InitMatch_NotInitialized_ReturnsNotTwoSameTeamNamesErrorMessage(Team firstTeam, Team secondTeam)
    {
        // Arrange
        var matchService = new MatchService();
        
        // Act
        Action action = () => matchService.InitMatch(firstTeam, secondTeam);
        
        //Assert
        action.Should().ThrowExactly<ArgumentException>().WithMessage("Two teams can't have the same names.");
    }

    [Fact]
    public void InitMatch_MatchInitialized_ReturnsCorrectMatch()
    {
        // Arrange
        var matchService = new MatchService();
        
        var firstMatchAwayTeam = new Team()
        {
            TeamName = "Mexico",
            TeamSide = TeamSides.AwayTeam
        };

        var firstMatchHomeTeam = new Team()
        {
            TeamName = "Canada",
            TeamSide = TeamSides.HomeTeam
        };
        var secondMatchAwayTeam = new Team()
        {
            TeamName = "Spin",
            TeamSide = TeamSides.AwayTeam
        };
        var secondMatchHomeTeam = new Team()
        {
            TeamName = "Brazil",
            TeamSide = TeamSides.HomeTeam
        };
        
        
        // Act
        var firstMatch = matchService.InitMatch(firstMatchAwayTeam, firstMatchHomeTeam);
        var secondMatch = matchService.InitMatch(secondMatchAwayTeam, secondMatchHomeTeam);

        // Assert
        firstMatch.MatchStatus.Should().Be(MatchStatuses.InProcess);
        firstMatch.AwayTeam.TeamScore.Should().Be(0);
        firstMatch.HomeTeam.TeamScore.Should().Be(0);
        
        secondMatch.MatchStatus.Should().Be(MatchStatuses.InProcess);
        secondMatch.AwayTeam.TeamScore.Should().Be(0);
        secondMatch.HomeTeam.TeamScore.Should().Be(0);
        
        matchService.ScoreBoard.Count.Should().Be(2);
    }
}