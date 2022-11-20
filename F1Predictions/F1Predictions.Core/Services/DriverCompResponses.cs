using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class DriverCompResponses : ICompResponses<Driver>
    {
        private readonly IRawCompResponses _rawResponses;
        private readonly IDriversDataService _driversService;

        public DriverCompResponses(IRawCompResponses rawResponses, IDriversDataService driversService)
        {
            _rawResponses = rawResponses;
            _driversService = driversService;
        }

        public List<Driver> GetAllResponses()
        {
            var rawResponses = _rawResponses.GetAllRawResponses();

            return rawResponses
                .Select(s => _driversService.FindItem(s))
                .ToList();
        }

        public Driver GetResponseForComp(string id)
        {
            var rawResponse = _rawResponses.GetRawResponseForComp(id);

            return _driversService.FindItem(rawResponse);
        }
    }
}
