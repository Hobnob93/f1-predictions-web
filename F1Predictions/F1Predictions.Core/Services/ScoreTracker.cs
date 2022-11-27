using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services
{
    public class ScoreTracker : IScoreTracker
    {
        public double Score { get; private set; }

        public void AddScore(double score)
        {
            Score += score;
        }
    }
}
