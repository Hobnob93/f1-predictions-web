using F1Predictions.Core.Enum;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class TeamsDataService : ITeamsDataService
    {
        private readonly IWebApiRequest _apiWebRequest;

        public List<Team> Data { get; private set; } = new();

        public TeamsDataService(IWebApiRequest apiWebRequest)
        {
            _apiWebRequest = apiWebRequest;
        }

        public Team FindItem(string id)
        {
            return Data.Single(d => string.Equals(d.Id, id, StringComparison.OrdinalIgnoreCase));
        }

        public async Task InitializeAsync()
        {
            Data = (await _apiWebRequest.GetAsync<IEnumerable<Team>>(ApiEndpoint.Teams))
                .ToList();
        }
    }
}
