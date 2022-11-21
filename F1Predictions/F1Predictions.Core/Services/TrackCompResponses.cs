using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class TrackCompResponses : ICompResponses<Track>, IMultiCompResponses<Track>
    {
        private readonly IRawCompResponses _rawResponses;
        private readonly ITracksDataService _tracksService;

        public TrackCompResponses(IRawCompResponses rawResponses, ITracksDataService trackService)
        {
            _rawResponses = rawResponses;
            _tracksService = trackService;
        }

        public List<Track> GetAllResponses()
        {
            var rawResponses = _rawResponses.GetAllRawResponses();

            return rawResponses
                .Select(s => _tracksService.FindItem(s))
                .ToList();
        }

        public Track GetResponseForComp(string id)
        {
            var rawResponse = _rawResponses.GetRawResponseForComp(id);

            return _tracksService.FindItem(rawResponse);
        }

        public List<List<Track>> GetAllMultiResponses()
        {
            var rawResponses = _rawResponses.GetAllRawResponses();

            return rawResponses
                .Select(str => TracksFromRaw(str)
                    .ToList())
                .ToList();
        }

        public List<Track> GetMultiResponseForComp(string id)
        {
            var rawResponse = _rawResponses.GetRawResponseForComp(id);

            return TracksFromRaw(rawResponse)
                .ToList();
        }

        private IEnumerable<Track> TracksFromRaw(string str)
        {
            return str
                .Split(",")
                .Select(str => _tracksService.FindItem(str));
        }
    }
}
