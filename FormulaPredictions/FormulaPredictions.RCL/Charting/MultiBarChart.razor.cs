using ApexCharts;
using FormulaPredictions.Shared.Models.Charting;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Charting;

public partial class MultiBarChart : BaseRclComponent
{
    [Parameter, EditorRequired]
    public MultiBarChartData Data { get; set; } = default!;

    [Parameter]
    public EventCallback<ChartDataPoint> SelectionCallback { get; set; }

    protected ApexChart<MultiBarChartDataPoint>? Chart;
    protected ApexChartOptions<MultiBarChartDataPoint> Options { get; set; } = new();

    protected override void OnInitialized()
    {
        Options.Tooltip = new Tooltip
        {
            Style = new TooltipStyle
            {
                FontSize = "14px"
            },
            Theme = Mode.Dark
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

    protected async Task OnChartItemSelected(SelectedData<MultiBarChartDataPoint> selectedData)
    {
        var index = selectedData.DataPointIndex;
        var item = Data.DataPoints[index];

        await SelectionCallback.InvokeAsync(item);
    }
}