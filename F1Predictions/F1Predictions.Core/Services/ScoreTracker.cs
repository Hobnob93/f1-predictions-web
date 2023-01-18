using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services
{
    public class ScoreTracker : IScoreTracker
    {
        private readonly ICompScoreTrackerFactory _trackerFactory;
        private readonly Dictionary<string, ICompScoreTracker> _scores;

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

        public double GetScore(string compId, string scoreId)
        {
            if (_scores.ContainsKey(compId))
                return _scores[compId].GetScore(scoreId);

            return 0;
        }

        public double GetTotalScore(string compId)
        {
            if (_scores.ContainsKey(compId))
                return _scores[compId].TotalScore;

            return 0;
        }
    }
}
