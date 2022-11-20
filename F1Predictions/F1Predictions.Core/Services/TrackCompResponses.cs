using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class TrackCompResponses : ICompResponses<Track>
    {
        private readonly IRawCompResponses _rawResponses;
        private readonly ITracksDataService _trackService;

        public TrackCompResponses(IRawCompResponses rawResponses, ITracksDataService trackService)
        {
            _rawResponses = rawResponses;
            _trackService = trackService;
        }

        public List<Track> GetAllResponses()
        {
            var rawResponses = _rawResponses.GetAllRawResponses();

            return rawResponses
                .Select(s => _trackService.FindItem(s))
                .ToList();
        }

        public Track GetResponseForComp(string id)
        {
            var rawResponse = _rawResponses.GetRawResponseForComp(id);

            return _trackService.FindItem(rawResponse);
        }
    }
}
