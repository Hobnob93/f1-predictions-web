using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Implementations.ScoringSystems;

public class GainWhenXScoringSystem : BaseScoringSystem, IScoringSystem
{
    public GainWhenXScoringSystem(IResponsesService responsesService)
        : base(responsesService)
    {
    }

    public double CalculateScoreForCompetitor(Competitor competitor, AppData appData, CurrentData current)
    {
        var (answerData, responses) = GetAnswerAndResponseItems(competitor, appData, current);

        var score = 0.0;
        foreach (var response in responses)
        {
            var selectedAnswer = answerData.AnswersData
                .SingleOrDefault(ad => string.Equals(ad.Id, response.Id, StringComparison.OrdinalIgnoreCase));

            if (selectedAnswer is null)
                continue;

            score += double.Parse(selectedAnswer.Value) * current.Question.Scoring.Value;
        }

        return score;
    }
}
