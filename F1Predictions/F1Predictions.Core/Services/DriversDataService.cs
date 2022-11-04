using F1Predictions.Core.Enum;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class DriversDataService : IDriversDataService
    {
        private readonly IWebApiRequest _apiWebRequest;

        public List<Driver> Data { get; private set; } = new();

        public DriversDataService(IWebApiRequest apiWebRequest)
        {
            _apiWebRequest = apiWebRequest;
        }

        public Driver FindItem(string id)
        {
            return Data.Single(d => string.Equals(d.Id, id, StringComparison.OrdinalIgnoreCase));
        }

        public async Task InitializeAsync()
        {
            Data = (await _apiWebRequest.GetAsync<IEnumerable<Driver>>(ApiEndpoint.Drivers))
                .OrderBy(t => t.Id)
                .ToList();
        }
    }
}
