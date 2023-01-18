namespace F1Predictions.Core.Interfaces
{
    public interface IScoreTracker
    {
        void AddScore(string compId, string scoreId, double score);
        double GetScore(string compId, string scoreId);
        double GetTotalScore(string compId);
    }
}
