using F1Predictions.Core.Enums;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class TracksDataService : BaseDataService<Track>, ITracksDataService
    {
        private readonly IWebApiRequest _apiWebRequest;

        public List<Track> Data { get; private set; } = new();

        public TracksDataService(IWebApiRequest apiWebRequest)
        {
            _apiWebRequest = apiWebRequest;
        }

        public Track FindItem(string id)
        {
            return FindItem(Data, id);
        }

        public async Task InitializeAsync()
        {
            Data = (await FetchFromApi(_apiWebRequest, ApiEndpoint.Tracks))
                .OrderBy(t => t.Id)
                .ToList();
        }
    }
}
