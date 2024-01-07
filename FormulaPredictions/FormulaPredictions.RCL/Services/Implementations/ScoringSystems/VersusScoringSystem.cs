using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Implementations.ScoringSystems;

public class VersusScoringSystem : BaseScoringSystem, IScoreSystem
{
    public VersusScoringSystem(IResponsesService responsesService)
        : base(responsesService)
    {
    }

    public double CalculateScoreForCompetitor(Competitor competitor, AppData appData, CurrentData current)
    {
        var (answerData, responses) = GetAnswerAndResponseItems(competitor, appData, current);
        var response = responses.First();

        return response.Id == answerData.AnswersData.First().Id ? 10 : 0;
    }
}
