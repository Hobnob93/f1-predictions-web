namespace F1Predictions.Core.Interfaces
{
    public interface IAnswerService
    {
        T GetCompetitorAnswer<T>(string id);
    }
}
