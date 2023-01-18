using ApexCharts;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Core
{
    public partial class MultiBarChart : BaseComponent, IRefreshable
    {
        [Parameter, EditorRequired]
        public string Title { get; set; } = string.Empty;

        [Parameter, EditorRequired]
        public MultiBarChartData Data { get; set; } = default!;

        [Parameter]
        public EventCallback<ChartDataPoint> SelectionCallback { get; set; }

        private ApexChart<MultiBarChartDataPoint>? Chart;
        private ApexChartOptions<MultiBarChartDataPoint> Options { get; set; } = new();

        protected override void OnInitialized()
        {
            Options.Tooltip = new Tooltip
            {
                Style = new TooltipStyle
                {
                    FontSize = "14px"
                },
                Theme = "dark"
            };

            Options.Legend = new Legend
            {
                FontSize = "14px",
                FontWeight = "bold",
                Position = LegendPosition.Bottom,
                Labels = new LegendLabels
                {
                    Colors = new Color("#9D9D9D")
                },
                Markers = new LegendMarkers
                {
                    FillColors = Data.DataPoints.Select(dp => dp.First().Color).ToList()
                }
            };
        }

        public async Task Refresh()
        {
            if (Chart is not null)
            {
                Options.Legend.Markers = new LegendMarkers
                {
                    FillColors = Data.DataPoints.Select(dp => dp.First().Color).ToList()
                };

                await Chart.UpdateSeriesAsync();
                await Chart.RenderAsync();
            }
        }

        private async Task OnChartItemSelected(SelectedData<MultiBarChartDataPoint> selectedData)
        {
            var item = selectedData.DataPoint.Items.ElementAt(selectedData.DataPointIndex);

            await SelectionCallback.InvokeAsync(item);
        }
    }
}
