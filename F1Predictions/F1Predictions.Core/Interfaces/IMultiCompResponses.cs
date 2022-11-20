namespace F1Predictions.Core.Interfaces
{
    public interface IMultiCompResponses<T>
    {
        List<List<T>> GetAllResponses();
        List<T> GetResponseForComp(string id);
    }
}
