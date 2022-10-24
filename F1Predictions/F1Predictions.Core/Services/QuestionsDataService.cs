using F1Predictions.Core.Enum;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class QuestionsDataService : IQuestionsDataService
    {
        private readonly IWebApiRequest _apiWebRequest;

        public List<QuestionResponse> Data { get; private set; } = new();

        public QuestionsDataService(IWebApiRequest apiWebRequest)
        {
            _apiWebRequest = apiWebRequest;
        }

        public QuestionResponse FindItem(string id)
        {
            return Data.Single(d => string.Equals(d.Id, id, StringComparison.OrdinalIgnoreCase));
        }

        public async Task InitializeAsync()
        {
            Data = (await _apiWebRequest.GetAsync<IEnumerable<QuestionResponse>>(ApiEndpoint.Questions))
                .ToList();
        }
    }
}
