using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace F1Predictions.Components.Question
{
    public partial class ValueContent : IRefreshable
    {
        [Inject]
        public IAnswerService AnswerService { get; set; } = default!;

        public List<ChartDataPoint> ResponsesData { get; private set; } = new();

        protected override void OnInitialized()
        {
            SetResponses();
        }

        private void SetResponses()
        {
            var answers = AnswerService
                .GetAnswersRaw()
                .Select(a => int.Parse(a))
                .OrderBy(a => a);

            var uniqueAnswers = answers.Distinct();

            ResponsesData = uniqueAnswers.Select(ua => new ChartDataPoint
            {
                Name = ua.ToString(),
                Value = answers.Count(a => ua == a)
            }).ToList();
        }

        private string GetCompetitorAnswer(string competitorId)
        {
            return AnswerService.GetCompetitorAnswerRaw(competitorId);
        }

        public async Task Refresh()
        {
            SetResponses();

            await InvokeAsync(StateHasChanged);
        }
    }
}
