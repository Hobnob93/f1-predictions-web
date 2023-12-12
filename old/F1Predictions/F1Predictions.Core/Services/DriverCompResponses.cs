using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class DriverCompResponses : ICompResponses<Driver>, IMultiCompResponses<Driver>
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
                .Select(str => _driversService.FindItem(str))
                .ToList();
        }

        public Driver GetResponseForComp(string id)
        {
            var rawResponse = _rawResponses.GetRawResponseForComp(id);

            return _driversService.FindItem(rawResponse);
        }

        public List<List<Driver>> GetAllMultiResponses()
        {
            var rawResponses = _rawResponses.GetAllRawResponses();

            return rawResponses
                .Select(str => DriversFromRaw(str)
                    .ToList())
                .ToList();
        }

        public List<Driver> GetMultiResponseForComp(string id)
        {
            var rawResponse = _rawResponses.GetRawResponseForComp(id);

            return DriversFromRaw(rawResponse)
                .ToList();
        }

        private IEnumerable<Driver> DriversFromRaw(string str)
        {
            return str
                .Split(",")
                .Select(str => _driversService.FindItem(str));
        }
    }
}
