namespace F1Predictions.Core.Interfaces
{
    public interface ICompResponses<T> where T: class
    {
        List<T> GetAllResponses();
        T GetResponseForComp(string id);
    }
}
