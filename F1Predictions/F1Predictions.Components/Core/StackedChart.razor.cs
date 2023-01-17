using ApexCharts;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Core
{
    public partial class StackedChart : BaseComponent, IRefreshable
    {
        [Parameter, EditorRequired]
        public string Title { get; set; } = string.Empty;

        [Parameter, EditorRequired]
        public StackedChartData Data { get; set; } = default!;

        [Parameter]
        public EventCallback<ChartDataPoint> SelectionCallback { get; set; }

        private ApexChart<ChartDataPoint>? Chart;
        private ApexChartOptions<ChartDataPoint> Options { get; set; } = new();

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

            Options.Chart = new Chart
            {
                Stacked = true,
                StackType = StackType.Normal
            };

            Options.PlotOptions = new PlotOptions
            {
                Bar = new PlotOptionsBar
                {
                    Horizontal = true
                }
            };
        }

        public async Task Refresh()
        {
            if (Chart is not null)
            {
                await Chart.UpdateSeriesAsync();
                await Chart.RenderAsync();
            }
        }

        private async Task OnChartItemSelected(SelectedData<ChartDataPoint> selectedData)
        {
            var item = selectedData.DataPoint.Items.ElementAt(selectedData.DataPointIndex);

            await SelectionCallback.InvokeAsync(item);
        }
    }
}
