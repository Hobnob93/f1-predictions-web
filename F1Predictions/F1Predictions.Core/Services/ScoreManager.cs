using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services
{
    public class ScoreManager : IScoreManager
    {
        private readonly IScoreSystemFactory _scoreSystemFactory;
        private readonly IScoreTracker _scoreTracker;
        private readonly ICompetitorsDataService _competitors;
        private readonly IQuestionsDataService _questions;

        public event Func<Task>? OnScoresUpdated;

        public ScoreManager(IScoreSystemFactory scoreSystemFactory, IScoreTracker scoreTracker, ICompetitorsDataService competitors, IQuestionsDataService questions)
        {
            _scoreSystemFactory = scoreSystemFactory;
            _scoreTracker = scoreTracker;
            _competitors = competitors;
            _questions = questions;
        }

        public async Task UpdateScoresForQuestion()
        {
            var curQuestion = _questions.CurrentQuestion;
            if (curQuestion.Type == Enums.QuestionType.Intro)
                return;

            var scoringSystem = _scoreSystemFactory.GetScoreSystem(curQuestion.Scoring.Type);

            if (scoringSystem is null)
                throw new InvalidOperationException($"Could not find scoring system for {curQuestion.Scoring.Type}");

            foreach (var comp in _competitors.Data)
            {
                var compScoreForQuestion = scoringSystem.GetScoreForComp(comp.Id);
                _scoreTracker.AddScore(comp.Id, curQuestion.Id, compScoreForQuestion);
            }

            if (OnScoresUpdated != null)
                await OnScoresUpdated.Invoke();
        }

        public double GetScore(string compId)
        {
            var curQuestion = _questions.CurrentQuestion;
            return _scoreTracker.GetScore(compId, curQuestion.Id);
        }
    }
}
