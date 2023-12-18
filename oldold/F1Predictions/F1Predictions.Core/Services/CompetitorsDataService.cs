using F1Predictions.Core.Enums;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class CompetitorsDataService : BaseDataService<Competitor>, ICompetitorsDataService
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
            return FindItem(Data, id);
        }

        public async Task InitializeAsync()
        {
            Data = (await FetchFromApi(_apiWebRequest, ApiEndpoint.Competitors))
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

        public async Task ShowAllWithAnswer(string answerId, IRawCompResponses answerService)
        {
            foreach (var d in Data)
            {
                var answer = answerService.GetRawResponseForComp(d.Id);
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
