using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.ScoringSystems
{
    public class GainLoseScoringSystem : IScoreSystem
    {
        private readonly IQuestionsDataService _questionsService;
        private readonly IAnswersDataService _answersService;
        private readonly IMultiCompResponses<DataItem> _responses;

        public GainLoseScoringSystem(IQuestionsDataService questionsService, IAnswersDataService answersService, IMultiCompResponses<DataItem> responses)
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
            var score = 0;
            foreach (var compResponse in compResponses)
            {
                if (answerData.AnswersData.Any(ad => ad.Id == compResponse.Id))
                {
                    score += 10;
                }
                else
                {
                    score -= 7;
                }
            }

            foreach (var answer in answerData.AnswersData)
            {
                if (compResponses.Any(r => r.Id == answer.Id))
                    continue;

                score -= 7;
            }

            return score;
        }
    }
}
