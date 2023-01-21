namespace F1Predictions.Core.Interfaces
{
    public interface IScoreManager
    {
        event Func<Task>? OnScoresUpdated;

        Task UpdateScoresForQuestion();
        double GetScore(string compId);
        double GetTotalScore(string compId);
        List<string> GetOrderedCompetitors();
    }
}
