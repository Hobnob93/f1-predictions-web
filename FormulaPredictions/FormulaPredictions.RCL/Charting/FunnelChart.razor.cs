using ApexCharts;

namespace FormulaPredictions.RCL.Charting;

public partial class FunnelChart : BaseChartComponent
{
    protected override void OnInitialized()
    {
        Options.Tooltip = new Tooltip
        {
            Enabled = false
        };

        Options.PlotOptions = new PlotOptions
        {
            Bar = new PlotOptionsBar
            {
                Horizontal = true,
                IsFunnel = true
            }
        };
    }
}