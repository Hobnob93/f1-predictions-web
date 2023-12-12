namespace F1Predictions.Core.Interfaces
{
    public interface ICompScoreTracker
    {
        double TotalScore { get; }

        void AddScore(string scoreId, double score);
        double GetScore(string scoreId);
    }
}
