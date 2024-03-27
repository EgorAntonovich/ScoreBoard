using FluentAssertions;
using ScoreBoard.Models;
using ScoreBoard.Services;

namespace ScoreBoard.Tests;

public class GetSummaryOfMatchesTests
{
    public static IEnumerable<object[]> GetMatchesGenerator()
    {
        yield return new object[] 
        {
            new List<Match>() {
                new Match()
                {
                    AwayTeam = new Team()
                    {
                        TeamName = "Mexico",
                        TeamSide = TeamSides.AwayTeam,
                        TeamScore = 0
                    },
                    HomeTeam = new Team()
                    {
                        TeamName = "Canada",
                        TeamSide = TeamSides.HomeTeam,
                        TeamScore = 5
                    },
                    MatchStartDate = new DateTime(2024,03,20),
                    MatchStatus = MatchStatuses.InProcess
                },
                new Match()
                {
                    AwayTeam = new Team()
                    {
                        TeamName = "Spain",
                        TeamSide = TeamSides.AwayTeam,
                        TeamScore = 10
                    },
                    HomeTeam = new Team()
                    {
                        TeamName = "Brazil",
                        TeamSide = TeamSides.HomeTeam,
                        TeamScore = 2
                    },
                    MatchStartDate = new DateTime(2024,03,15),
                    MatchStatus = MatchStatuses.InProcess
                },
                new Match()
                {
                    AwayTeam = new Team()
                    {
                        TeamName = "Germany",
                        TeamSide = TeamSides.AwayTeam,
                        TeamScore = 2
                    },
                    HomeTeam = new Team()
                    {
                        TeamName = "France",
                        TeamSide = TeamSides.HomeTeam,
                        TeamScore = 2
                    },
                    MatchStartDate = new DateTime(2024,03,21),
                    MatchStatus = MatchStatuses.InProcess
                },
                new Match()
                {
                    AwayTeam = new Team()
                    {
                        TeamName = "Uruguay",
                        TeamSide = TeamSides.AwayTeam,
                        TeamScore = 6
                    },
                    HomeTeam = new Team()
                    {
                        TeamName = "Italy",
                        TeamSide = TeamSides.HomeTeam,
                        TeamScore = 6
                    },
                    MatchStartDate = new DateTime(2024,03,10),
                    MatchStatus = MatchStatuses.InProcess
                },
                new Match()
                {
                    AwayTeam = new Team()
                    {
                        TeamName = "Argentina",
                        TeamSide = TeamSides.AwayTeam,
                        TeamScore = 3
                    },
                    HomeTeam = new Team()
                    {
                        TeamName = "Australia",
                        TeamSide = TeamSides.HomeTeam,
                        TeamScore = 1
                    },
                    MatchStartDate = new DateTime(2024,03,20),
                    MatchStatus = MatchStatuses.InProcess
                },
            
            },
            new List<Match>() {
                new Match()
                {
                    AwayTeam = new Team()
                    {
                        TeamName = "Uruguay",
                        TeamSide = TeamSides.AwayTeam,
                        TeamScore = 6
                    },
                    HomeTeam = new Team()
                    {
                        TeamName = "Italy",
                        TeamSide = TeamSides.HomeTeam,
                        TeamScore = 6
                    },
                    MatchStartDate = new DateTime(2024,03,10),
                    MatchStatus = MatchStatuses.InProcess
                },
                new Match()
                {
                    AwayTeam = new Team()
                    {
                        TeamName = "Spain",
                        TeamSide = TeamSides.AwayTeam,
                        TeamScore = 10
                    },
                    HomeTeam = new Team()
                    {
                        TeamName = "Brazil",
                        TeamSide = TeamSides.HomeTeam,
                        TeamScore = 2
                    },
                    MatchStartDate = new DateTime(2024,03,15),
                    MatchStatus = MatchStatuses.InProcess
                },
                new Match()
                {
                    AwayTeam = new Team()
                    {
                        TeamName = "Mexico",
                        TeamSide = TeamSides.AwayTeam,
                        TeamScore = 0
                    },
                    HomeTeam = new Team()
                    {
                        TeamName = "Canada",
                        TeamSide = TeamSides.HomeTeam,
                        TeamScore = 5
                    },
                    MatchStartDate = new DateTime(2024,03,20),
                    MatchStatus = MatchStatuses.InProcess
                },
                new Match()
                {
                    AwayTeam = new Team()
                    {
                        TeamName = "Argentina",
                        TeamSide = TeamSides.AwayTeam,
                        TeamScore = 3
                    },
                    HomeTeam = new Team()
                    {
                        TeamName = "Australia",
                        TeamSide = TeamSides.HomeTeam,
                        TeamScore = 1
                    },
                    MatchStartDate = new DateTime(2024,03,20),
                    MatchStatus = MatchStatuses.InProcess
                },
                new Match()
                {
                    AwayTeam = new Team()
                    {
                        TeamName = "Germany",
                        TeamSide = TeamSides.AwayTeam,
                        TeamScore = 2
                    },
                    HomeTeam = new Team()
                    {
                        TeamName = "France",
                        TeamSide = TeamSides.HomeTeam,
                        TeamScore = 2
                    },
                    MatchStartDate = new DateTime(2024,03,21),
                    MatchStatus = MatchStatuses.InProcess
                },
            }
        };
    }

    [Fact]
    public void GetSummaryOfMatches_GetSummaryFailed_ReturnsExceptionCantGetSummaryOfEmptyScoreBoard()
    {
        // Arrange
        var scoreBoardService = new ScoreBoardService();
        
        // Act
        Action action = () => scoreBoardService.GetSummaryOfMatches();
        
        // Assert
        action.Should().ThrowExactly<Exception>().WithMessage("There are no any active matches.");
    }

    [Theory]
    [MemberData(nameof(GetMatchesGenerator))]
    public void GetSummaryOfMatches_GetSummarySucceed_ScoreBoardListSortedCorrectly(List<Match> scoreBoard, List<Match> correctOrderedScreBoard)
    {
        // Arrange
        var scoreBoardService = new ScoreBoardService();
        
        // Act
        foreach (var match in scoreBoard)
        {
            scoreBoardService.InitMatch(match.HomeTeam, match.AwayTeam);
        }
        var orderedScoreBoard = scoreBoardService.GetSummaryOfMatches();
        var compareResult = orderedScoreBoard.SequenceEqual(correctOrderedScreBoard);

        // Assert
        compareResult.Should().Be(true);
    }
}