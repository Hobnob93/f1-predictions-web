using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services
{
    public class ScoreManager : IScoreManager
    {
        private readonly IScoreSystemFactory _scoreSystemFactory;
        private readonly IScoreTracker _scoreTracker;
        private readonly ICompetitorsDataService _competitors;
        private readonly IQuestionsDataService _questions;

        public ScoreManager(IScoreSystemFactory scoreSystemFactory, IScoreTracker scoreTracker, ICompetitorsDataService competitors, IQuestionsDataService questions)
        {
            _scoreSystemFactory = scoreSystemFactory;
            _scoreTracker = scoreTracker;
            _competitors = competitors;
            _questions = questions;
        }

        public void UpdateScoresForQuestion()
        {
            var curQuestion = _questions.CurrentQuestion;
            var scoringSystem = _scoreSystemFactory.GetScoreSystem(curQuestion.Scoring.Type);

            if (scoringSystem is null)
                throw new InvalidOperationException($"Could not find scoring system for {curQuestion.Scoring.Type}");

            foreach (var comp in _competitors.Data)
            {
                var compScoreForQuestion = scoringSystem.GetScoreForComp(comp.Id);
                _scoreTracker.AddScore(comp.Id, curQuestion.Id, compScoreForQuestion);
            }
        }
    }
}
