using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services
{
    public class CompScoreTracker : ICompScoreTracker
    {
        private readonly Dictionary<string, double> _scores = new();

        public double TotalScore => _scores.Sum(kvp => kvp.Value);

        public void AddScore(string scoreId, double score)
        {
            if (_scores.ContainsKey(scoreId))
            {
                _scores[scoreId] = score;
                return;
            }

            _scores.Add(scoreId, score);
        }

        public double GetScore(string scoreId)
        {
            if (_scores.ContainsKey(scoreId))
                return _scores[scoreId];

            return 0;
        }
    }
}
