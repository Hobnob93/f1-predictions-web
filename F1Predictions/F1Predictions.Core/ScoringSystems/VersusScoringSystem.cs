using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.ScoringSystems
{
    public class VersusScoringSystem : IScoreSystem
    {
        private readonly IQuestionsDataService _questionsService;
        private readonly IAnswersDataService _answersService;
        private readonly ICompResponses<DataItem> _responses;

        public VersusScoringSystem(IQuestionsDataService questionsService, IAnswersDataService answersService, ICompResponses<DataItem> responses)
        {
            _questionsService = questionsService;
            _answersService = answersService;
            _responses = responses;
        }

        public double GetScoreForComp(string compId)
        {
            var answerIdForCurrentQuestion = _questionsService.CurrentQuestion.Scoring.AnswersId;
            var answersData = _answersService.FindItem(answerIdForCurrentQuestion).AnswersData;

            var compResponse = _responses.GetResponseForComp(compId);
            return compResponse.Id == answersData.First().Id ? 10 : 0;
        }
    }
}
