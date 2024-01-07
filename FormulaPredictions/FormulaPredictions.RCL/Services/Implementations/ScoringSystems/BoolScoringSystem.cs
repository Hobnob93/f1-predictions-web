using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Implementations.ScoringSystems;

public class BoolScoringSystem : IScoreSystem
{
    public double CalculateScoreForCompetitor(Competitor competitor, AppData appData, CurrentData current)
    {
        var answerId = current.Question.Scoring.AnswersId;
        var answerData = appData.Answers.Single(a => string.Equals(a.Id, answerId, StringComparison.OrdinalIgnoreCase));

        var competitorResponse = current.Question.CompetitorResponses.Single(c => string.Equals(c.Id, answerId, StringComparison.OrdinalIgnoreCase));
        return string.Equals(competitorResponse.Id, answerData.RawAnswer, StringComparison.OrdinalIgnoreCase)
            ? 10 : 0;
    }
}
