using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace F1Predictions.Components.Question
{
    public class QuestionContent : ComponentBase, IRefreshable
    {
        [Inject]
        protected ICompetitorsDataService CompetitorsService { get; set; } = default!;

        [Inject]
        protected IAnswerService AnswerService { get; set; } = default!;

        public List<ChartDataPoint> ResponseData { get; protected set; } = new();
        public ChartOptions ChartOptions { get; protected set; } = new();

        protected int _selectedChartIndex;

        protected override void OnInitialized()
        {
            SetResponses();
        }

        protected virtual void SetResponses()
        {
            _selectedChartIndex = -1;
        }

        public async Task Refresh()
        {
            SetResponses();

            await InvokeAsync(StateHasChanged);
        }

        protected async Task OnSelectedChartIndex(int index)
        {
            _selectedChartIndex = index;
            if (_selectedChartIndex == -1)
                return;

            var responseData = ResponseData[_selectedChartIndex];

            await CompetitorsService.ShowAllWithAnswer(responseData.Id, AnswerService);
        }
    }
}
