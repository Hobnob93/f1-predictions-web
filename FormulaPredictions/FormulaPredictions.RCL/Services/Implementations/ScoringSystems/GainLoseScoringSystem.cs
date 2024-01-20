using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Implementations.ScoringSystems;

public class GainLoseScoringSystem : BaseScoringSystem, IScoringSystem
{
    public GainLoseScoringSystem(IResponsesService responsesService)
        : base(responsesService)
    {
    }

    public double CalculateScoreForCompetitor(Competitor competitor, AppData appData, CurrentData current)
    {
        var (answerData, responses) = GetAnswerAndResponseItems(competitor, appData, current);
        if (answerData.AnswersData.Length == 0)
        {
            var rawAnswers = answerData.RawAnswer?.Split(",");

            if (rawAnswers is null || rawAnswers.Length == 0)
                throw new InvalidOperationException($"AnswersData for '{answerData.Id}' has not been provided!"); ;

            answerData.AnswersData = rawAnswers
                .Select((rr, i) => new AnswerData { Id = rr, Value = i.ToString() })
                .ToArray();
        }

        var score = 0;
        foreach (var response in responses)
        {
            if (answerData.AnswersData.Any(ad => ad.Id == response.Id))
            {
                score += current.Question.Scoring.Value;
            }
            else
            {
                score += current.Question.Scoring.ExtraValue;
            }
        }

        foreach (var answer in answerData.AnswersData)
        {
            if (responses.Any(r => r.Id == answer.Id))
                continue;

            score += current.Question.Scoring.ExtraValue;
        }

        return score;
    }
}
