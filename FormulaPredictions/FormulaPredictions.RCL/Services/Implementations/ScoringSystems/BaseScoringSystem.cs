using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.Models.Base;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Implementations.ScoringSystems;

public abstract class BaseScoringSystem
{
    protected readonly IResponsesService _responsesService;

    public BaseScoringSystem(IResponsesService responsesService)
    {
        _responsesService = responsesService;
    }

    protected (Answer Answer, CompetitorResponse Response) GetAnswerAndResponse(Competitor competitor, AppData appData, CurrentData current)
    {
        var answerData = GetAnswerData(appData, current);
        var competitorResponse = current.Question.CompetitorResponses.Single(c => string.Equals(c.Id, competitor.Id, StringComparison.OrdinalIgnoreCase));

        return (answerData, competitorResponse);
    }

    protected (Answer Answer, BaseItem[] Items) GetAnswerAndResponseItems(Competitor competitor, AppData appData, CurrentData current)
    {
        var answerData = GetAnswerData(appData, current);
        var responseItems = _responsesService.GetAllResponses(competitor.Id, appData, current);

        return (answerData, responseItems);
    }

    protected Answer GetAnswerData(AppData appData, CurrentData current)
    {
        var answerId = current.Question.Scoring.AnswersId;
        return appData.Answers.Single(a => string.Equals(a.Id, answerId, StringComparison.OrdinalIgnoreCase));
    }
}
