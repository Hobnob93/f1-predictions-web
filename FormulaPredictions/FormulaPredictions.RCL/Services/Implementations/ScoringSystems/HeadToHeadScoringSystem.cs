using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.Models.Base;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Implementations.ScoringSystems;

public class HeadToHeadScoringSystem : BaseScoringSystem, IScoringSystem
{
    public HeadToHeadScoringSystem(IResponsesService responsesService)
        : base(responsesService)
    {
    }

    public double CalculateScoreForCompetitor(Competitor competitor, AppData appData, CurrentData current)
    {
        var (answerData, responses) = GetAnswerAndResponseItems(competitor, appData, current);

        var score = 0d;
        score += GetQualiScore(responses, answerData);
        score += GetRacingScore(responses, answerData);

        return score;
    }

    private double GetQualiScore(BaseItem[] responses, Answer answerData)
    {
        if (answerData.AnswersData[0].Value == answerData.AnswersData[1].Value)
            return 10;

        return responses.First().Id == answerData.AnswersData[0].Id ? 10 : 0;
    }

    private double GetRacingScore(BaseItem[] responses, Answer answerData)
    {
        if (answerData.AnswersData[2].Value == answerData.AnswersData[3].Value)
            return 10;

        return responses.Last().Id == answerData.AnswersData[2].Id ? 10 : 0;
    }
}
