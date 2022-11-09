using ApexCharts;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Core
{
    public partial class PieChart : BaseComponent
    {
        [Parameter, EditorRequired]
        public string Title { get; set; } = string.Empty;

        [Parameter, EditorRequired]
        public IEnumerable<ChartDataPoint> Data { get; set; } = default!;

        private ApexChart<ChartDataPoint>? Chart;
    }
}
