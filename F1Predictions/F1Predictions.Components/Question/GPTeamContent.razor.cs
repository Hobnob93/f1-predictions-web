using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace F1Predictions.Components.Question
{
    public partial class GPTeamContent
    {
        [Inject]
        public ITeamsDataService TeamsService { get; set; } = default!;

        [Inject]
        public IAnswerService AnswerService { get; set; } = default!;

        public List<ChartDataPoint> AnswersData { get; private set; } = new();
        public ChartOptions ChartOptions { get; private set; } = new();

        protected override void OnInitialized()
        {
            SetFavouriteLiveries();

            base.OnInitialized();
        }

        private void SetFavouriteLiveries()
        {
            var teamIds = AnswerService.GetAnswersRaw();
            var teams = teamIds
                .Distinct()
                .Select(id => TeamsService.FindItem(id))
                .ToList();

            ChartOptions.ChartPalette = teams
                .Select(t => t.Color)
                .ToArray();

            AnswersData = teams.Select(t => new ChartDataPoint
            {
                Name = t.Name,
                Value = teamIds.Count(id => id == t.Id)
            }).ToList();
        }

        private string GetCompetitorAnswer(string competitorId)
        {
            var answer = AnswerService.GetCompetitorAnswerRaw(competitorId);

            return TeamsService.Data.Single(t => t.Id == answer).Name;
        }
    }
}
