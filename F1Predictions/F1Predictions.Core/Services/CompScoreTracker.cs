using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services
{
    public class CompScoreTracker : ICompScoreTracker
    {
        private readonly List<string> _scoreIds = new();

        public double Score { get; private set; }

        public void AddScore(string scoreId, double score)
        {
            if (_scoreIds.Contains(scoreId))
                return;

            _scoreIds.Add(scoreId);
            Score += score;
        }
    }
}
