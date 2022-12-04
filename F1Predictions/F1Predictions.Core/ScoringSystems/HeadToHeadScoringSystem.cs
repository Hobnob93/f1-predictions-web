using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.ScoringSystems
{
    public class HeadToHeadScoringSystem : IScoreSystem
    {
        private readonly IQuestionsDataService _questionsService;
        private readonly IAnswersDataService _answersService;
        private readonly IMultiCompResponses<DataItem> _responses;

        public HeadToHeadScoringSystem(IQuestionsDataService questionsService, IAnswersDataService answersService, IMultiCompResponses<DataItem> responses)
        {
            _questionsService = questionsService;
            _answersService = answersService;
            _responses = responses;
        }

        public double GetScoreForComp(string compId)
        {
            var answerIdForCurrentQuestion = _questionsService.CurrentQuestion.Scoring.AnswersId;
            var answersData = _answersService.FindItem(answerIdForCurrentQuestion).AnswersData;

            var compResponses = _responses.GetMultiResponseForComp(compId);
            var score = 0;
            score += compResponses.First().Id == answersData[0].Id ? 10 : 0;
            score += compResponses.Last().Id == answersData[2].Id ? 10 : 0;

            return score;
        }
    }
}
