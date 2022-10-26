using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace F1Predictions.Components.Question
{
    public partial class IntroContent
    {
        [Inject]
        public ITeamsDataService TeamsService { get; set; } = default!;

        [Inject]
        public IAnswerService AnswerService { get; set; } = default!;

        public List<ChartDataPoint> FavouriteLiveriesData { get; private set; } = new();
        public ChartOptions ChartOptions { get; private set; } = new();

        protected override void OnInitialized()
        {
            SetFavouriteLiveries();

            base.OnInitialized();
        }

        private void SetFavouriteLiveries()
        {
            var liveryIds = AnswerService.GetAnswersRaw();
            var teamsFromLiveries = liveryIds
                .Distinct()
                .Select(id => TeamsService.FindItem(id))
                .ToList();

            ChartOptions.ChartPalette = teamsFromLiveries
                .Select(t => t.Color)
                .ToArray();

            FavouriteLiveriesData = teamsFromLiveries.Select(t => new ChartDataPoint
            {
                Name = t.Name,
                Value = liveryIds.Count(id => id == t.Id)
            }).ToList();
        }
    }
}
