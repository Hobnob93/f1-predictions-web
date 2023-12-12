using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.ScoringSystems
{
    public class MultiDriverOnTrackScoringSystem : IScoreSystem
    {
        private readonly IQuestionsDataService _questionsService;
        private readonly IAnswersDataService _answersService;
        private readonly IMultiCompResponses<DriverTrack> _responses;

        public MultiDriverOnTrackScoringSystem(IQuestionsDataService questionsService, IAnswersDataService answersService, IMultiCompResponses<DriverTrack> responses)
        {
            _questionsService = questionsService;
            _answersService = answersService;
            _responses = responses;
        }

        public double GetScoreForComp(string compId)
        {
            var answerIdForCurrentQuestion = _questionsService.CurrentQuestion.Scoring.AnswersId;
            var answersData = _answersService.FindItem(answerIdForCurrentQuestion).AnswersData;
            var answersBreakdown = ScoresBreakdownForAnswersData(answersData);

            var compResponses = _responses.GetMultiResponseForComp(compId);
            var score = 0.0;
            foreach (var compResponse in compResponses)
            {
                var trackBreakdown = answersBreakdown[compResponse.Track.Id];
                score += trackBreakdown[compResponse.Driver.Id];
            }

            return score;
        }

        private Dictionary<string, Dictionary<string, int>> ScoresBreakdownForAnswersData(List<AnswerData> answersData)
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
}
