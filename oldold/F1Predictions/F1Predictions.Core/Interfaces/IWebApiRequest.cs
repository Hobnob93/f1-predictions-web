using F1Predictions.Core.Enums;

namespace F1Predictions.Core.Interfaces
{
    public interface IWebApiRequest
    {
        Task<T> GetAsync<T>(ApiEndpoint apiEndpoint, params object[] parameters);
    }
}
