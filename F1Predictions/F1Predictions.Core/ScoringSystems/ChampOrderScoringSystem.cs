using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.ScoringSystems
{
    public class ChampOrderScoringSystem : IScoreSystem
    {
        private readonly IQuestionsDataService _questionsService;
        private readonly IAnswersDataService _answersService;

        public ChampOrderScoringSystem(IQuestionsDataService questionsService, IAnswersDataService answersService)
        {
            _questionsService = questionsService;
            _answersService = answersService;
        }

        public double ScoreForCompResponse(string compResponse)
        {
            return 0;
        }

        public double ExtraToAccountFor(string compId)
        {
            return 0;
        }
    }
}
