namespace ScoreBoard.Tests;

public class StartNewMatchTests
{
    private readonly IMatchService _matchService;

    public StartNewMatchTests(IMatchService matchService)
    {
        _matchService = matchService;
    }
    
    [Fact]
    [Theory]
    [InlineData("Mexico","Canada")]
    [InlineData("Spain","Brazil")]
    [InlineData("Germany","France")]
    [InlineData("Uruguay","Italy")]
    [InlineData("Argentina","Australia")]
    public void InitMatch_NotInited_ReturnsNullOrEmptyTeamError(string firstTeam, string secondTeam)
    {
        // Arrange
        var initedMatches = matchService.InitedMatches;
        
        //Act
        var errorMessage = matchService.InitMatch(firstTeam, secondTeam);

        //Assert
        Assert.False(errorMessage, "One ot more teams can't be empty");
    }
}