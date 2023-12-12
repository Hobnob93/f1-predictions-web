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
            var answerData = _answersService.FindItem(answerIdForCurrentQuestion) 
                ?? throw new InvalidOperationException($"AnswersData for '{answerIdForCurrentQuestion}' has not been provided!"); ;

            if (answerData.AnswersData is null || answerData.AnswersData.Count == 0)
            {
                var rawAnswers = answerData.RawAnswer?.Split(",");

                if (rawAnswers is null || rawAnswers.Length == 0)
                    throw new InvalidOperationException($"AnswersData for '{answerIdForCurrentQuestion}' has not been provided!"); ;

                answerData.AnswersData = rawAnswers
                    .Select((rr, i) => new AnswerData { Id = rr, Value = i.ToString() })
                    .ToList();
            }

            var compResponses = _responses.GetMultiResponseForComp(compId);
            var score = 0;
            foreach (var compResponse in compResponses)
            {
                if (answerData.AnswersData.Any(ad => ad.Id == compResponse.Id))
                {
                    score += _questionsService.CurrentQuestion.Scoring.Value;
                }
                else
                {
                    score += _questionsService.CurrentQuestion.Scoring.ExtraValue;
                }
            }

            foreach (var answer in answerData.AnswersData)
            {
                if (compResponses.Any(r => r.Id == answer.Id))
                    continue;

                score += _questionsService.CurrentQuestion.Scoring.ExtraValue;
            }

            return score < 0 ? 0 : score;
        }
    }
}
