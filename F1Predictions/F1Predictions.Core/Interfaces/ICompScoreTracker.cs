namespace F1Predictions.Core.Interfaces
{
    public interface ICompScoreTracker
    {
        double Score { get; }

        void AddScore(string scoreId, double score);
    }
}
