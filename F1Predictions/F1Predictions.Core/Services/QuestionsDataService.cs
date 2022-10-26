using F1Predictions.Core.Enum;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class QuestionsDataService : IQuestionsDataService
    {
        private readonly IWebApiRequest _apiWebRequest;

        public List<QuestionResponse> Data { get; private set; } = new();
        public QuestionResponse? CurrentQuestion { get; private set; }

        public event Func<Task>? StateChanged;

        private int _currentIndex;

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

            _currentIndex = 0;
            await UpdateCurrentQuestion(notify: false);
        }

        public bool CanGoForward()
        {
            return _currentIndex < Data.Count - 1;
        }

        public bool CanGoBack()
        {
            return _currentIndex > 0;
        }

        public async Task Next()
        {
            _currentIndex++;
            await UpdateCurrentQuestion();
        }

        public async Task Previous()
        {
            _currentIndex--;
            await UpdateCurrentQuestion();
        }

        private async Task UpdateCurrentQuestion(bool notify = true)
        {
            CurrentQuestion = Data[_currentIndex];

            if (notify && StateChanged is not null)
                await StateChanged.Invoke();
        }
    }
}
