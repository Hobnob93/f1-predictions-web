namespace F1Predictions.Core.Interfaces
{
    public interface IScoreSystem
    {
        void AddScore(string compId, string scoreId, double score);
    }
}
