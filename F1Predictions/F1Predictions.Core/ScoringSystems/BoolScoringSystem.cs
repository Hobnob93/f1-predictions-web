using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.ScoringSystems
{
    public class BoolScoringSystem : IScoreSystem
    {
        private readonly IQuestionsDataService _questionsService;
        private readonly IAnswersDataService _answersService;
        private readonly ICompResponses<DataItem> _responses;

        public BoolScoringSystem(IQuestionsDataService questionsService, IAnswersDataService answersService, ICompResponses<DataItem> responses)
        {
            _questionsService = questionsService;
            _answersService = answersService;
            _responses = responses;
        }

        public double GetScoreForComp(string compId)
        {
            var answerIdForCurrentQuestion = _questionsService.CurrentQuestion.Scoring.AnswersId;
            var answerData = _answersService.FindItem(answerIdForCurrentQuestion);

            var compResponse = _responses.GetResponseForComp(compId);
            return compResponse.Id == answerData.RawAnswer ? 10 : 0;
        }
    }
}
