using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using System.Net;

namespace F1Predictions.Core.ScoringSystems
{
    public class ValueScoringSystem : IScoreSystem
    {
        private readonly IQuestionsDataService _questionsService;
        private readonly IAnswersDataService _answersService;
        private readonly IRawCompResponses _responses;

        public ValueScoringSystem(IQuestionsDataService questionsService, IAnswersDataService answersService, IRawCompResponses responses)
        {
            _questionsService = questionsService;
            _answersService = answersService;
            _responses = responses;
        }

        public double GetScoreForComp(string compId)
        {
            var answerIdForCurrentQuestion = _questionsService.CurrentQuestion.Scoring.AnswersId;
            var answerData = _answersService.FindItem(answerIdForCurrentQuestion);

            var answer = int.Parse(answerData.RawAnswer ?? throw new InvalidOperationException($"Answer for '{answerData.Id}' has not been provided!"));
            var compResponseRaw = _responses.GetRawResponseForComp(compId);
            var compResponse = int.Parse(compResponseRaw);

            var score = 25 - Math.Abs(answer - compResponse);
            return score < 0 ? 0 : score;
        }
    }
}
