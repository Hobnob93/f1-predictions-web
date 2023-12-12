namespace F1Predictions.Core.Interfaces
{
    public interface IMultiCompResponses<T>
    {
        List<List<T>> GetAllMultiResponses();
        List<T> GetMultiResponseForComp(string id);
    }
}
