namespace F1Predictions.Core.Interfaces
{
    public interface IScoreTracker
    {
        double Score { get; }

        void AddScore(string scoreId, double score);
    }
}
