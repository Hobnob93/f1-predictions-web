using F1Predictions.Core.Enum;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class DriversDataService : BaseDataService<Driver>, IDriversDataService
    {
        private readonly IWebApiRequest _apiWebRequest;

        public List<Driver> Data { get; private set; } = new();

        public DriversDataService(IWebApiRequest apiWebRequest)
        {
            _apiWebRequest = apiWebRequest;
        }

        public Driver FindItem(string id)
        {
            return FindItem(Data, id);
        }

        public async Task InitializeAsync()
        {
            Data = (await FetchFromApi(_apiWebRequest, ApiEndpoint.Drivers))
                .OrderBy(t => t.Id)
                .ToList();
        }
    }
}
