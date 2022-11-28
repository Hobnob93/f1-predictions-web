using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services
{
    public class ScoreSystem : IScoreSystem
    {
        private readonly IScoreTrackerFactory _trackerFactory;

        private Dictionary<string, IScoreTracker> _scores;

        public ScoreSystem(IScoreTrackerFactory trackerFactory)
        {
            _trackerFactory = trackerFactory;
            _scores = new();
        }

        public void AddScore(string compId, string scoreId, double score)
        {
            if (!_scores.ContainsKey(compId))
            {
                _scores.Add(compId, _trackerFactory.CreateTracker());
            }

            _scores[compId].AddScore(scoreId, score);
        }
    }
}
