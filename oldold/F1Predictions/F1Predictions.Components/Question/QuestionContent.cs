using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Question
{
    public abstract class QuestionContent : ComponentBase, IRefreshable
    {
        [Inject]
        protected ICompetitorsDataService CompetitorsService { get; set; } = default!;

        [Inject]
        protected IRawCompResponses CompResponses { get; set; } = default!;

        [Inject]
        protected IScoreManager ScoreManager { get; set; } = default!;

        public IRefreshable? ResponsesChart { get; set; }
        public List<ChartDataPoint> ResponseData { get; protected set; } = new();

        public IRefreshable? AnswersSection { get; set; }

        protected abstract void SetResponses();

        protected override void OnInitialized()
        {
            SetResponses();
        }

        public async Task Refresh()
        {
            SetResponses();

            await InvokeAsync(StateHasChanged);

            if (ResponsesChart is not null)
                await ResponsesChart.Refresh();

            if (AnswersSection is not null)
                await AnswersSection.Refresh();
        }

        protected async Task OnSelectedChartDataPoint(ChartDataPoint dataPoint)
        {
            await CompetitorsService.ShowAllWithAnswer(dataPoint.Id, CompResponses);
        }
    }
}
