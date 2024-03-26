using FluentAssertions;
using ScoreBoard.Models;
using ScoreBoard.Services;

namespace ScoreBoard.Tests;

public class StartNewMatchTests()
{
    public static IEnumerable<object[]> GetTeamsDataWithSameSidesGenerator()
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
                TeamSide = TeamSides.HomeTeam
            },
            new Team()
            {
                TeamName = "Mexico",
                TeamSide = TeamSides.AwayTeam
            },
        };
    }
    
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
                TeamName = "Spain",
                TeamSide = TeamSides.HomeTeam
            },
        };
    }
    
    [Theory]
    [MemberData(nameof(GetTeamsDataWithSameSidesGenerator))]
    public void InitMatch_NotInitialized_ReturnsNotTwoSameTeamSidesErrorMessage(Team firstTeam, Team secondTeam)
    {
        //Arrange
        var matchService = new ScoreBoardService();
        
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
        var matchService = new ScoreBoardService();
        
        // Act
        Action action = () => matchService.InitMatch(firstTeam, secondTeam);
        
        //Assert
        action.Should().ThrowExactly<ArgumentException>().WithMessage("Two teams can't have the same names.");
    }
    
    [Theory]
    [MemberData(nameof(GetTeamsDataWithInvalidSidesGenerator))]
    public void InitMatch_NotInitialized_ReturnsArgumentExceptionInvalidTeamsSideMessage(Team firstTeam, Team secondTeam)
    {
        // Arrange
        var matchService = new ScoreBoardService();
        
        // Act
        Action action = () => matchService.InitMatch(firstTeam, secondTeam);
        
        //Assert
        action.Should().ThrowExactly<ArgumentException>().WithMessage("Invalid teams side. First argument should be home team and second - away team");
    }

    [Fact]
    public void InitMatch_MatchInitialized_ReturnsCorrectMatch()
    {
        // Arrange
        var scoreBoardService = new ScoreBoardService();
        
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
        var firstMatch = scoreBoardService.InitMatch(firstMatchHomeTeam, firstMatchAwayTeam);
        var secondMatch = scoreBoardService.InitMatch(secondMatchHomeTeam, secondMatchAwayTeam);

        // Assert
        firstMatch.MatchStatus.Should().Be(MatchStatuses.InProcess);
        firstMatch.AwayTeam.TeamScore.Should().Be(0);
        firstMatch.HomeTeam.TeamScore.Should().Be(0);
        
        secondMatch.MatchStatus.Should().Be(MatchStatuses.InProcess);
        secondMatch.AwayTeam.TeamScore.Should().Be(0);
        secondMatch.HomeTeam.TeamScore.Should().Be(0);
        
        scoreBoardService.ScoreBoard.Count.Should().Be(2);
    }
}