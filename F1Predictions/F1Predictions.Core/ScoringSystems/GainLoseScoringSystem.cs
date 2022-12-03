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

        public double ScoreForCompResponse(string compResponse)
        {
            var answerIdForCurrentQuestion = _questionsService.CurrentQuestion.Scoring.AnswersId;
            var answerData = _answersService.FindItem(answerIdForCurrentQuestion);

            return compResponse == answerData.RawAnswer ? 10 : -7;
        }

        public double ExtraToAccountFor(string compId)
        {
            var missingCount = 0;
            var compResponses = _responses.GetMultiResponseForComp(compId);

            var answerIdForCurrentQuestion = _questionsService.CurrentQuestion.Scoring.AnswersId;
            var answerData = _answersService.FindItem(answerIdForCurrentQuestion);
            foreach (var answer in answerData.AnswersData)
            {
                if (compResponses.Any(r => r.Id == answer.Id))
                    continue;

                missingCount++;
            }

            return missingCount * -7;
        }
    }
}
