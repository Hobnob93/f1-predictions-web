using ApexCharts;
using FormulaPredictions.Shared.Models.Charting;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Charting;

public partial class StackedChart : BaseRclComponent
{
    [Parameter, EditorRequired]
    public StackedChartData Data { get; set; } = default!;

    [Parameter]
    public EventCallback<ChartDataPoint> SelectionCallback { get; set; }

    protected ApexChart<ChartDataPoint>? Chart;
    protected ApexChartOptions<ChartDataPoint> Options { get; set; } = new();

    protected async Task OnChartItemSelected(SelectedData<ChartDataPoint> selectedData)
    {
        var item = selectedData.DataPoint.Items.First();
        await SelectionCallback.InvokeAsync(item);
    }

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
}