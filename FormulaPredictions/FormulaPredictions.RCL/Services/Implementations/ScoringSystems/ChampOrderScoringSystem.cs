using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Implementations.ScoringSystems;

public class ChampOrderScoringSystem : BaseScoringSystem, IScoreSystem
{
    public ChampOrderScoringSystem(IResponsesService responsesService)
        : base(responsesService)
    {
    }

    public double CalculateScoreForCompetitor(Competitor competitor, AppData appData, CurrentData current)
    {
        var (answerData, responses) = GetAnswerAndResponseItems(competitor, appData, current);
        var leaderboard = answerData.AnswersData;
        if (leaderboard is null || leaderboard.Length == 0)
            throw new InvalidOperationException($"AnswersData for '{answerData.Id}' has not been provided!");

        var score = 0.0;
        var currentIndex = (current.Question.Scoring.Index ?? 0) - 1;
        foreach (var response in responses)
        {
            currentIndex++;

            var indexOfResponse = Array.FindIndex(leaderboard, 0, leaderboard.Length, 
                a => string.Equals(a.Id, response.Id, StringComparison.OrdinalIgnoreCase));
            if (indexOfResponse == -1)
                continue;

            score += GetScoreFromLeaderboardPositionDistance(indexOfResponse - currentIndex);
        }

        return score;
    }

    private double GetScoreFromLeaderboardPositionDistance(int distance)
    {
        return Math.Abs(distance) switch
        {
            0 => 25,
            1 => 18,
            2 => 15,
            3 => 12,
            4 => 10,
            5 => 8,
            6 => 6,
            7 => 4,
            8 => 2,
            9 => 1,
            _ => 0
        };
    }
}
