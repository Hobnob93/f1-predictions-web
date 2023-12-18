namespace F1Predictions.Core.Interfaces
{
    public interface IRawCompResponses
    {
        List<string> GetAllRawResponses();
        string GetRawResponseForComp(string id);
    }
}
