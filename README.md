# ScoreBoard

ScoreBoard is the simple library for Live Football
World Cup that shows all the ongoing matches and their scores.

## Content

- [Install and setup](#install-and-setup)
- [Using samples](#using-samples)
- [Documentation](#documentation)
- [Types](#types)
- [List of messages](#list-of-messages)
- [Contact information](#contact-information)
- [TODO improvments](#todo-improvments)

## Install and setup

It's just simple library wich you can clone and ad to your project.

Technology stack of main project:
- .Net8
  
Technology stack of tests project:
- .Net8
- XUnit 2.5.3
- FluentAssertions 6.12

## Using samples

```csharp
using ScoreBoard;

// Create new entity of Score Board Service
ScoreBoardService scoreBoardService = new ScoreBoardService();

// Init new match
scoreBoardService.InitMatch(Team homeTeam, Team awayTeam);

// Update match score
scoreBoardService.UpdateScore(Team homeTeam, Team awayTeam);

// Finish match
scoreBoardService.FinishMatch(Team homeTeam, Team awayTeam);

// Get summary of matches
var matchStatistics = scoreBoardService.GetSummaryOfMatches();
```

## Documentation

ScoreBoard library provides simple functional and consists of 4 actions:

- InitMatch
- UpdateMatch
- FinishMatch
- GetSummaryOfMatches

### InitMatch 
> -> Take 2 arguments as parameters of [Team type](#team-type) wich should describe teams.
As default scores of match will be 0 - 0. And match status will be setted up as InProgress. Returns result as [ProcessResult type](#processresult-type).

### UpdateMatch
> -> Take 2 arguments as parameters of [Team type](#team-type) wich should describe existed teams and new score. Returns result as [ProcessResult type](#processresult-type).

### FinishMatch 
> -> Take 2 arguments as parameters of [Team type](#team-type) wich should describe existed teams in match wich should be ended.
Match status will be changed to Completed. And this match will not participate in summary calculating. Returns result as [ProcessResult type](#processresult-type).

### GetSummaryOfMatches 
> -> Returns list of matches with status InProgress and ordered by their total score. 
The matches with the same total score will be returned ordered by the most recently started match in thescoreboard. Returns List of Match type.

## Types

### Match type
```csharp
    public Team HomeTeam { get; set; }
    public Team AwayTeam { get; set; }
    public MatchStatuses MatchStatus { get; set; }
```
### Team type
```csharp
    public string TeamName { get; set; }
    public TeamSides TeamSide { get; set; }
    public int TeamScore { get; set; } = 0;
```
### ProcessResult type
```csharp
    public record  ProcessResult(bool Flag, string Message, Match Data)
```
  > Flag - true if method completed and false if method failed.

  > Message - message wich describes action result.

  > Data - returns Match if action was succeed and null in other way.

## List of messages

### Succeess messages
> Match has been successfully created.
> Match has been successfully updated.
> Match has been successfully finished.

### Failed messages
> Two teams can't have the same names.
> Two teams can't have the same sides.
> Invalid teams side. First argument should be home team and second - away team.
> There is not such match.
> There is not such match for finish.
> There are no any active matches.

## Contact information

Author: Egor Antonovich
Email: antonovich.egor1@gmail.com

## TODO Improvments
- Refactor Update match parameters. To -> ```csharp scoreBoardService.UpdateScore(string TeamName, int newScore);```
> Because only one team with unique name could be InProcess. In this case not necessary check and update two teams. Or overload this method and use 2 business logics. Add unit tests for this.

- Add Logger to monitoring actions of this service.

