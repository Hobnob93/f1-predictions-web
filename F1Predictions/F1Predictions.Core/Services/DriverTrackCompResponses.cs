using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class DriverTrackCompResponses : IMultiCompResponses<DriverTrack>
    {
        private readonly IRawCompResponses _rawResponses;
        private readonly IDriversDataService _driversService;
        private readonly ITracksDataService _tracksService;

        public DriverTrackCompResponses(IRawCompResponses rawResponses, IDriversDataService driversService, ITracksDataService tracksService)
        {
            _rawResponses = rawResponses;
            _driversService = driversService;
            _tracksService = tracksService;
        }

        public List<List<DriverTrack>> GetAllResponses()
        {
            var rawResponses = _rawResponses.GetAllRawResponses();

            return rawResponses
                .Select(str => DriverTracksFromRaw(str)
                    .ToList())
                .ToList();
        }

        public List<DriverTrack> GetResponseForComp(string id)
        {
            var rawResponse = _rawResponses.GetRawResponseForComp(id);

            return DriverTracksFromRaw(rawResponse)
                .ToList();
        }

        private IEnumerable<DriverTrack> DriverTracksFromRaw(string rawAnswer)
        {
            return rawAnswer
                .Split(",")
                .Select(str => (str.Split("-").First(), str.Split("-").Last()))
                .Select(tuple => new DriverTrack(_driversService.FindItem(tuple.Item1), _tracksService.FindItem(tuple.Item2)));
        }
    }
}
