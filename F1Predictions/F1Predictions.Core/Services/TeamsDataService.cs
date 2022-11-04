using F1Predictions.Core.Enum;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class TeamsDataService : BaseDataService<Team>, ITeamsDataService
    {
        private readonly IWebApiRequest _apiWebRequest;

        public List<Team> Data { get; private set; } = new();

        public TeamsDataService(IWebApiRequest apiWebRequest)
        {
            _apiWebRequest = apiWebRequest;
        }

        public Team FindItem(string id)
        {
            return FindItem(Data, id);
        }

        public async Task InitializeAsync()
        {
            Data = (await FetchFromApi(_apiWebRequest, ApiEndpoint.Teams))
                .OrderBy(t => t.Name)
                .ToList();
        }
    }
}
