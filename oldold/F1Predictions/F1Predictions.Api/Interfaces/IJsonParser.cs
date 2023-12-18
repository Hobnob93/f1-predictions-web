namespace F1Predictions.Api.Interfaces
{
    public interface IJsonParser
    {
        Task<T> ParseAsync<T>(string fileLocation) where T : class;
    }
}
