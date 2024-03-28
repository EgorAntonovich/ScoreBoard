using FluentAssertions;
using ScoreBoard.Models;
using ScoreBoard.Models.Enums;
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
        var initMatchResult = matchService.InitMatch(firstTeam, secondTeam);
        
        //Assert
        initMatchResult.Data.Should().Be(null);
        initMatchResult.Flag.Should().Be(false);
        initMatchResult.Message.Should().Be("Two teams can't have the same sides.");
    }
    
    [Theory]
    [MemberData(nameof(GetTeamsDataWithSameNamesGenerator))]
    public void InitMatch_NotInitialized_ReturnsNotTwoSameTeamNamesErrorMessage(Team firstTeam, Team secondTeam)
    {
        // Arrange
        var matchService = new ScoreBoardService();
        
        // Act
        var initMatchResult = matchService.InitMatch(firstTeam, secondTeam);
        
        //Assert
        initMatchResult.Data.Should().Be(null);
        initMatchResult.Flag.Should().Be(false);
        initMatchResult.Message.Should().Be("Two teams can't have the same names.");
    }
    
    [Theory]
    [MemberData(nameof(GetTeamsDataWithInvalidSidesGenerator))]
    public void InitMatch_NotInitialized_ReturnsArgumentExceptionInvalidTeamsSideMessage(Team firstTeam, Team secondTeam)
    {
        // Arrange
        var matchService = new ScoreBoardService();
        
        // Act
        var initMatchResult =  matchService.InitMatch(firstTeam, secondTeam);
        
        //Assert
        initMatchResult.Data.Should().Be(null);
        initMatchResult.Flag.Should().Be(false);
        initMatchResult.Message.Should().Be("Invalid teams side. First argument should be home team and second - away team.");
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
        var firstMatchProcessResult = scoreBoardService.InitMatch(firstMatchHomeTeam, firstMatchAwayTeam);
        var secondMatchProcessResult = scoreBoardService.InitMatch(secondMatchHomeTeam, secondMatchAwayTeam);

        // Assert
        firstMatchProcessResult.Data.MatchStatus.Should().Be(MatchStatuses.InProcess);
        firstMatchProcessResult.Data.AwayTeam.TeamScore.Should().Be(0);
        firstMatchProcessResult.Data.HomeTeam.TeamScore.Should().Be(0);
        firstMatchProcessResult.Flag.Should().Be(true);
        firstMatchProcessResult.Message.Should().Be("Match has been successfully created.");
        
        secondMatchProcessResult.Data.MatchStatus.Should().Be(MatchStatuses.InProcess);
        secondMatchProcessResult.Data.AwayTeam.TeamScore.Should().Be(0);
        secondMatchProcessResult.Data.HomeTeam.TeamScore.Should().Be(0);
        secondMatchProcessResult.Flag.Should().Be(true);
        secondMatchProcessResult.Message.Should().Be("Match has been successfully created.");
        
        scoreBoardService.ScoreBoard.Count.Should().Be(2);
    }
}