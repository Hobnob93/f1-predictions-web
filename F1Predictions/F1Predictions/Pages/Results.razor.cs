using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace F1Predictions.Pages
{
    public partial class Results
    {
        [Inject]
        public ICompetitorsDataService CompetitorsService { get; set; } = default!;

        [Inject]
        public ITeamsDataService TeamsService { get; set; } = default!;

        public string[] FavouriteLabels => FavouriteLiveriesData.Select(d => d.Name).ToArray();
        public double[] FavouriteData => FavouriteLiveriesData.Select(d => d.Value).ToArray();

        public List<ChartDataPoint> FavouriteLiveriesData { get; private set; } = new();
        public ChartOptions ChartOptions { get; private set; } = new();

        protected override void OnInitialized()
        {
            SetFavouriteLiveries();

            base.OnInitialized();
        }

        private void SetFavouriteLiveries()
        {
            var liveryIds = CompetitorsService.Data.Select(d => d.LiveryId);
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
