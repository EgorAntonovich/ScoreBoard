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
            new Team() {TeamName = "Mexico", TeamSide = TeamSides.AwayTeam},
            new Team() {TeamName = "Canada", TeamSide = TeamSides.AwayTeam},
        };
        yield return new object[]
        {
            new Team() {TeamName = "Mexico", TeamSide = TeamSides.HomeTeam},
            new Team() {TeamName = "Canada", TeamSide = TeamSides.HomeTeam},
        };
    } 
    
    public static IEnumerable<object[]> GetTeamsDataWithSameNamesGenerator()
    {
        yield return new object[] 
        {
            new Team() {TeamName = "Mexico", TeamSide = TeamSides.AwayTeam},
            new Team() {TeamName = "Mexico", TeamSide = TeamSides.AwayTeam},
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
}