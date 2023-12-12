namespace F1Predictions.Core.Interfaces
{
    public interface ICompResponses<T>
    {
        List<T> GetAllResponses();
        T GetResponseForComp(string id);
    }
}
