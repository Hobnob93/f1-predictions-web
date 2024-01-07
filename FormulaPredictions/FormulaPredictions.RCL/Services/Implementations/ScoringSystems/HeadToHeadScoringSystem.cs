using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Implementations.ScoringSystems;

public class HeadToHeadScoringSystem : BaseScoringSystem, IScoreSystem
{
    public HeadToHeadScoringSystem(IResponsesService responsesService)
        : base(responsesService)
    {
    }

    public double CalculateScoreForCompetitor(Competitor competitor, AppData appData, CurrentData current)
    {
        var (answerData, responses) = GetAnswerAndResponseItems(competitor, appData, current);

        var score = 0;
        score += responses.First().Id == answerData.AnswersData[0].Id ? 10 : 0;
        score += responses.Last().Id == answerData.AnswersData[2].Id ? 10 : 0;

        return score;
    }
}
