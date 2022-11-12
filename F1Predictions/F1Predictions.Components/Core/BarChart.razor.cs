using ApexCharts;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Core
{
    public partial class BarChart : BaseComponent
    {
        [Parameter, EditorRequired]
        public string Title { get; set; } = string.Empty;

        [Parameter, EditorRequired]
        public List<ChartDataPoint> Data { get; set; } = default!;

        [Parameter, EditorRequired]
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
        }

        private async Task OnChartItemSelected(SelectedData<ChartDataPoint> selectedData)
        {
            var index = selectedData.DataPointIndex;
            var item = Data[index];

            await SelectionCallback.InvokeAsync(item);
        }
    }
}
