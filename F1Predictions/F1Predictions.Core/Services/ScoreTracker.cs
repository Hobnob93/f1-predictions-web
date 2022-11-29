using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services
{
    public class ScoreTracker : IScoreTracker
    {
        private readonly ICompScoreTrackerFactory _trackerFactory;

        private Dictionary<string, ICompScoreTracker> _scores;

        public ScoreTracker(ICompScoreTrackerFactory trackerFactory)
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
