using F1Predictions.Core.Enum;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class CompetitorsDataService : ICompetitorsDataService
    {
        private readonly IWebApiRequest _apiWebRequest;

        public event Func<Task>? ShowingStateChanged;

        public List<Competitor> Data { get; private set; } = new();

        public CompetitorsDataService(IWebApiRequest apiWebRequest)
        {
            _apiWebRequest = apiWebRequest;
        }

        public Competitor FindItem(string id)
        {
            return Data.Single(d => string.Equals(d.Id, id, StringComparison.OrdinalIgnoreCase));
        }

        public async Task InitializeAsync()
        {
            Data = (await _apiWebRequest.GetAsync<IEnumerable<Competitor>>(ApiEndpoint.Competitors))
                .OrderBy(d => d.Id)
                .ToList();
        }

        public async Task ResetShowingStates()
        {
            foreach (var d in Data)
            {
                d.IsShowingContent = false;
            }

            await NotifyShowingStateChanged();
        }

        public async Task ShowAllWithAnswer(string answerId, IAnswerService answerService)
        {
            foreach (var d in Data)
            {
                var answer = answerService.GetCompetitorAnswerRaw(d.Id);
                if (string.Equals(answer, answerId))
                {
                    d.IsShowingContent = true;
                }
            }

            await NotifyShowingStateChanged();
        }

        public async Task ShowCompetitor(string competitorId)
        {
            var item = FindItem(competitorId);
            item.IsShowingContent = true;

            await NotifyShowingStateChanged();
        }

        private async Task NotifyShowingStateChanged()
        {
            if (ShowingStateChanged is not null)
                await ShowingStateChanged.Invoke();
        }
    }
}
