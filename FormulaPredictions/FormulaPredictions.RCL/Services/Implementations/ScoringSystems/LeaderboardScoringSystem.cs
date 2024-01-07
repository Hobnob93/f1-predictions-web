using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Implementations.ScoringSystems;

public class LeaderboardScoringSystem : BaseScoringSystem, IScoreSystem
{
    public LeaderboardScoringSystem(IResponsesService responsesService)
        : base(responsesService)
    {
    }

    public double CalculateScoreForCompetitor(Competitor competitor, AppData appData, CurrentData current)
    {
        var (answerData, responses) = GetAnswerAndResponseItems(competitor, appData, current);

        var leaderboard = answerData.AnswersData;
        if (leaderboard.Length == 0)
        {
            var rawResponses = answerData.RawAnswer?.Split(",");

            if (rawResponses is null || rawResponses.Length == 0)
                throw new InvalidOperationException($"AnswersData for '{answerData.Id}' has not been provided!");

            leaderboard = rawResponses
                .Select((rr, i) => new AnswerData { Id = rr, Value = i.ToString() })
                .ToArray();
        }

        var response = responses.First();
        var indexOfResponse = Array.FindIndex(leaderboard, 0, leaderboard.Length,
            a => string.Equals(a.Id, response.Id, StringComparison.OrdinalIgnoreCase));

        if (indexOfResponse == -1)
            return 0;

        return GetScoreFromLeaderboardPositionDistance(indexOfResponse - current.Question.Scoring.Index ?? 0);
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
