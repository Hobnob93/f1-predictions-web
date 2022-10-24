using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace F1Predictions.Components.Core
{
    public partial class PieChart : BaseComponent
    {
        [Parameter, EditorRequired]
        public string Title { get; set; } = string.Empty;

        [Parameter, EditorRequired]
        public List<ChartDataPoint> Data { get; set; } = new();

        [Parameter]
        public ChartOptions Options { get; set; } = new();

        private string[] Labels => Data.Select(d => d.Name).ToArray();
        private double[] DataPoints => Data.Select(d => d.Value).ToArray();
    }
}
