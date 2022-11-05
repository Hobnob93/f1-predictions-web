using F1Predictions.Core.Enums;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public abstract class BaseDataService<T> where T : DataItem
    {
        public T FindItem(IEnumerable<T> data, string id)
        {
            try
            {
                return data.Single(d => string.Equals(d.Id, id, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                ex.Data.Add("ID to find", id);

                throw;
            }
        }

        public async Task<IEnumerable<T>> FetchFromApi(IWebApiRequest apiWebRequest, ApiEndpoint apiEndpoint)
        {
            return await apiWebRequest.GetAsync<IEnumerable<T>>(apiEndpoint);
        }
    }
}
