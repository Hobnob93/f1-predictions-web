using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class HeadToHeadCompResponses : ICompResponses<HeadToHead>
    {
        private readonly IRawCompResponses _rawResponses;
        private readonly IDriversDataService _driversService;

        public HeadToHeadCompResponses(IRawCompResponses rawResponses, IDriversDataService driversService)
        {
            _rawResponses = rawResponses;
            _driversService = driversService;
        }

        public List<HeadToHead> GetAllResponses()
        {
            var rawResponses = _rawResponses.GetAllRawResponses();

            return rawResponses
                .Select(str => HeadToHeadFromRaw(str))
                .ToList();
        }

        public HeadToHead GetResponseForComp(string id)
        {
            var rawResponse = _rawResponses.GetRawResponseForComp(id);

            return HeadToHeadFromRaw(rawResponse);
        }

        private HeadToHead HeadToHeadFromRaw(string str)
        {
            var components = str.Split(",");

            var qualiChoice = _driversService.FindItem(components.First());
            var raceChoice = _driversService.FindItem(components.Last());

            return new HeadToHead(qualiChoice, raceChoice);
        }
    }
}
