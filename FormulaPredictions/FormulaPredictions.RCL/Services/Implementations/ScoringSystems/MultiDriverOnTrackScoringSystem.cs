using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Implementations.ScoringSystems;

public class MultiDriverOnTrackScoringSystem : BaseScoringSystem, IScoreSystem
{
    public MultiDriverOnTrackScoringSystem(IResponsesService responsesService)
        : base(responsesService)
    {
    }

    public double CalculateScoreForCompetitor(Competitor competitor, AppData appData, CurrentData current)
    {
        var answerData = GetAnswerData(appData, current);
        var answersBreakdown = ScoresBreakdownForAnswersData(answerData.AnswersData);

        var responses = _responsesService.GetDriverTrackResponses(competitor.Id, appData, current);
        var score = 0.0;
        foreach (var response in responses)
        {
            var trackBreakdown = answersBreakdown[response.Circuit.Id];
            score += trackBreakdown[response.Driver.Id];
        }

        return score;
    }

    private Dictionary<string, Dictionary<string, int>> ScoresBreakdownForAnswersData(AnswerData[] answersData)
    {
        var breakdownDictionary = new Dictionary<string, Dictionary<string, int>>();

        foreach (var answerData in answersData)
        {
            var driversDictionary = new Dictionary<string, int>();
            var driverAnswers = answerData.Value.Split(",");

            foreach (var driverAnswer in driverAnswers)
            {
                var answerComponents = driverAnswer.Split("-");
                driversDictionary.Add(answerComponents[0], int.Parse(answerComponents[1]));
            }

            breakdownDictionary.Add(answerData.Id, driversDictionary);
        }

        return breakdownDictionary;
    }
}
