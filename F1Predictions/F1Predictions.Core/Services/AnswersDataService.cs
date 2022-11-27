using F1Predictions.Core.Enums;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class AnswersDataService : BaseDataService<Answer>, IAnswersDataService
    {
        private readonly IWebApiRequest _apiWebRequest;

        public List<Answer> Data { get; private set; } = new();

        public AnswersDataService(IWebApiRequest apiWebRequest)
        {
            _apiWebRequest = apiWebRequest;
        }

        public Answer FindItem(string id)
        {
            return FindItem(Data, id);
        }

        public async Task InitializeAsync()
        {
            Data = (await FetchFromApi(_apiWebRequest, ApiEndpoint.Teams))
                .ToList();
        }
    }
}
