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
            var liveryNames = liveryIds.Select(id => TeamsService.FindItem(id).Name).ToList();

            ChartOptions.ChartPalette = TeamsService.Data.Select(t => t.Color).ToArray();

            FavouriteLiveriesData = TeamsService.Data.Select(t => new ChartDataPoint
            {
                Name = t.Name,
                Value = liveryNames.Count(ln => ln == t.Name)
            }).ToList();
        }
    }
}
