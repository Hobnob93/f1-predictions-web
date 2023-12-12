using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.ScoringSystems
{
    public class GainWhenXScoringSystem : IScoreSystem
    {
        private readonly IQuestionsDataService _questionsService;
        private readonly IAnswersDataService _answersService;
        private readonly IMultiCompResponses<DataItem> _responses;

        public GainWhenXScoringSystem(IQuestionsDataService questionsService, IAnswersDataService answersService, IMultiCompResponses<DataItem> responses)
        {
            _questionsService = questionsService;
            _answersService = answersService;
            _responses = responses;
        }

        public double GetScoreForComp(string compId)
        {
            var answerIdForCurrentQuestion = _questionsService.CurrentQuestion.Scoring.AnswersId;
            var answerData = _answersService.FindItem(answerIdForCurrentQuestion);

            var compResponses = _responses.GetMultiResponseForComp(compId);
            var score = 0.0;
            foreach (var compResponse in compResponses)
            {
                var selectedAnswer = answerData.AnswersData.SingleOrDefault(ad => ad.Id == compResponse.Id);
                if (selectedAnswer is null)
                    continue;

                score += double.Parse(selectedAnswer.Value) * _questionsService.CurrentQuestion.Scoring.Value;
            }

            return score;
        }
    }
}
