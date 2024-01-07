using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Implementations.ScoringSystems;

public class ValueScoringSystem : BaseScoringSystem, IScoreSystem
{
    public ValueScoringSystem(IResponsesService responsesService)
        : base(responsesService)
    {
    }

    public double CalculateScoreForCompetitor(Competitor competitor, AppData appData, CurrentData current)
    {
        var answerData = GetAnswerData(appData, current);
        var answer = int.Parse(answerData.RawAnswer 
            ?? throw new InvalidOperationException($"Answer for '{answerData.Id}' has not been provided!"));

        var response = _responsesService.GetValueResponse<int>(competitor.Id, current);
        var score = 25 - Math.Abs(answer - response);
        return score < 0 ? 0 : score;
    }
}
