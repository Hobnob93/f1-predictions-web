using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services
{
    public class CompScoreTrackerFactory : ICompScoreTrackerFactory
    {
        public ICompScoreTracker CreateTracker()
        {
            return new CompScoreTracker();
        }
    }
}
