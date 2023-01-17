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
                StackType = StackType.Percent100,
                Toolbar = new Toolbar
                {
                    Show = false
                }
            };

            Options.PlotOptions = new PlotOptions
            {
                Bar = new PlotOptionsBar
                {
                    Horizontal = true
                }
            };

            Options.Legend = new Legend
            {
                FontSize = "14px",
                FontWeight = "bold",
                Position = LegendPosition.Right,
                Labels = new LegendLabels
                {
                    Colors = new Color("#9D9D9D")
                },
                Markers = new LegendMarkers
                {
                    FillColors = new List<string> { Data.LeftStackData.First().Color, Data.RightStackData.First().Color }
                }
            };

            Options.DataLabels = new DataLabels
            {
                Enabled = true,
                Style = new DataLabelsStyle
                {
                    FontSize = "20px"
                }
            };
        }

        public async Task Refresh()
        {
            if (Chart is not null)
            {
                Options.Legend.Markers = new LegendMarkers
                {
                    FillColors = new List<string> { Data.LeftStackData.First().Color, Data.RightStackData.First().Color }
                };

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
