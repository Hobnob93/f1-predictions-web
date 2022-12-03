using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class AnyCompResponses : ICompResponses<DataItem>, IMultiCompResponses<DataItem>
    {
        private readonly IRawCompResponses _rawResponses;

        public AnyCompResponses(IRawCompResponses rawResponses)
        {
            _rawResponses = rawResponses;
        }

        public List<DataItem> GetAllResponses()
        {
            var rawResponses = _rawResponses.GetAllRawResponses();

            return rawResponses
                .Select(str => new DataItem { Id = str })
                .ToList();
        }

        public DataItem GetResponseForComp(string id)
        {
            var rawResponse = _rawResponses.GetRawResponseForComp(id);

            return new DataItem { Id = rawResponse };
        }

        public List<List<DataItem>> GetAllMultiResponses()
        {
            var rawResponses = _rawResponses.GetAllRawResponses();

            return rawResponses
                .Select(str => DataItemFromRaw(str)
                    .ToList())
                .ToList();
        }

        public List<DataItem> GetMultiResponseForComp(string id)
        {
            var rawResponse = _rawResponses.GetRawResponseForComp(id);

            return DataItemFromRaw(rawResponse)
                .ToList();
        }

        private IEnumerable<DataItem> DataItemFromRaw(string str)
        {
            return str
                .Split(",")
                .Select(str => new DataItem { Id = str });
        }
    }
}
