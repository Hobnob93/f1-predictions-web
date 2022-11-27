using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services
{
    public class ScoreTrackerFactory : IScoreTrackerFactory
    {
        public IScoreTracker CreateTracker()
        {
            return new ScoreTracker();
        }
    }
}
