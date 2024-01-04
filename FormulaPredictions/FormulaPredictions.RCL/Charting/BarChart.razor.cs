using ApexCharts;

namespace FormulaPredictions.RCL.Charting;

public partial class BarChart : BaseChartComponent
{
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
    }
}