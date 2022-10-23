using F1Predictions.Core.Enum;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class CompetitorsDataService : ICompetitorsDataService
    {
        private readonly IWebApiRequest _apiWebRequest;

        public List<Competitor> Data { get; private set; } = new();

        public CompetitorsDataService(IWebApiRequest apiWebRequest)
        {
            _apiWebRequest = apiWebRequest;
        }

        public Competitor FindItem(string id)
        {
            return Data.Single(d => string.Equals(d.Id, id, StringComparison.OrdinalIgnoreCase));
        }

        public async Task InitializeAsync()
        {
            Data = (await _apiWebRequest.GetAsync<IEnumerable<Competitor>>(ApiEndpoint.Competitors))
                .OrderBy(d => d.Id)
                .ToList();
        }
    }
}
