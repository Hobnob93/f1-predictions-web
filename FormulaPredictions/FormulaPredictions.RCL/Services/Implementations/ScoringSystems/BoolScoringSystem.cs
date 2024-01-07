using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Implementations.ScoringSystems;

public class BoolScoringSystem : BaseScoringSystem, IScoringSystem
{
    public BoolScoringSystem(IResponsesService responsesService)
        : base(responsesService)
    {
    }

    public double CalculateScoreForCompetitor(Competitor competitor, AppData appData, CurrentData current)
    {
        var (answerData, competitorResponse) = GetAnswerAndResponse(competitor, appData, current);
        return string.Equals(competitorResponse.Id, answerData.RawAnswer, StringComparison.OrdinalIgnoreCase)
            ? 10 : 0;
    }
}
