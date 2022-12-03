using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.ScoringSystems
{
    public class GetChoiceValueScoringSystem : IScoreSystem
    {
        private readonly IQuestionsDataService _questionsService;
        private readonly IAnswersDataService _answersService;

        public GetChoiceValueScoringSystem(IQuestionsDataService questionsService, IAnswersDataService answersService)
        {
            _questionsService = questionsService;
            _answersService = answersService;
        }

        public double ScoreForCompResponse(string compResponse)
        {
            var answerIdForCurrentQuestion = _questionsService.CurrentQuestion.Scoring.AnswersId;
            var answerData = _answersService.FindItem(answerIdForCurrentQuestion);
            var selectedAnswer = answerData.AnswersData.SingleOrDefault(ad => ad.Id == compResponse);
            if (selectedAnswer is null)
                return 0;

            return double.Parse(selectedAnswer.Value);
        }

        public double ExtraToAccountFor(string compId)
        {
            return 0;
        }
    }
}
