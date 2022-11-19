using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace F1Predictions.Components.Question
{
    public abstract class QuestionContent : ComponentBase, IRefreshable
    {
        [Inject]
        protected ICompetitorsDataService CompetitorsService { get; set; } = default!;

        [Inject]
        protected IAnswerService AnswerService { get; set; } = default!;

        public List<ChartDataPoint> ResponseData { get; protected set; } = new();

        protected abstract void SetResponses();

        protected override void OnInitialized()
        {
            SetResponses();
        }

        public async Task Refresh()
        {
            SetResponses();

            await InvokeAsync(StateHasChanged);
        }

        protected async Task OnSelectedChartDataPoint(ChartDataPoint dataPoint)
        {
            await CompetitorsService.ShowAllWithAnswer(dataPoint.Id, AnswerService);
        }
    }
}
