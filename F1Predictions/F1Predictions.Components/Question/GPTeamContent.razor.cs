using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace F1Predictions.Components.Question
{
    public partial class GPTeamContent : IRefreshable
    {
        [Inject]
        public ITeamsDataService TeamsService { get; set; } = default!;

        [Inject]
        public IAnswerService AnswerService { get; set; } = default!;

        public List<ChartDataPoint> ResponseData { get; private set; } = new();
        public ChartOptions ChartOptions { get; private set; } = new();

        protected override void OnInitialized()
        {
            SetResponses();
        }

        private void SetResponses()
        {
            var teamIds = AnswerService.GetAnswersRaw();
            var teams = teamIds
                .Distinct()
                .Select(id => TeamsService.FindItem(id))
                .ToList();

            ChartOptions.ChartPalette = teams
                .Select(t => t.Color)
                .ToArray();

            ResponseData = teams.Select(t => new ChartDataPoint
            {
                Name = t.Name,
                Value = teamIds.Count(id => id == t.Id)
            }).ToList();
        }

        private (string Id, string Name) GetCompetitorAnswer(string competitorId)
        {
            var answerId = AnswerService.GetCompetitorAnswerRaw(competitorId);
            var answer = TeamsService.Data.Single(t => t.Id == answerId).Name;

            return (answerId, answer);
        }

        public async Task Refresh()
        {
            SetResponses();

            await InvokeAsync(StateHasChanged);
        }
    }
}
