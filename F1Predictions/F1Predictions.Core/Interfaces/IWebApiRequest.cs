using F1Predictions.Core.Enum;

namespace F1Predictions.Core.Interfaces
{
    public interface IWebApiRequest
    {
        Task<T> GetAsync<T>(ApiEndpoint apiEndpoint, params object[] parameters);
    }
}
