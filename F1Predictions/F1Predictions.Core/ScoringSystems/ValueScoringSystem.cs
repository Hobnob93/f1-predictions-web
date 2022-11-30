using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.ScoringSystems
{
    public class ValueScoringSystem : IScoreSystem
    {
        private readonly IQuestionsDataService _questionsService;
        private readonly IAnswersDataService _answersService;

        public ValueScoringSystem(IQuestionsDataService questionsService, IAnswersDataService answersService)
        {
            _questionsService = questionsService;
            _answersService = answersService;
        }

        public double ScoreForCompResponse(string compResponse)
        {
            var answerIdForCurrentQuestion = _questionsService.CurrentQuestion.Scoring.AnswersId;
            var answerData = _answersService.FindItem(answerIdForCurrentQuestion);

            var answer = int.Parse(answerData.RawAnswer ?? throw new InvalidOperationException($"Answer for '{answerData.Id}' has not been provided!"));
            var response = int.Parse(compResponse);

            return 25 - Math.Abs(answer - response);
        }
    }
}
