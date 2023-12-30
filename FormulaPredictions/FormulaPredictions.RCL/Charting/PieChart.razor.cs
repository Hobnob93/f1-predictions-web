using ApexCharts;

namespace FormulaPredictions.RCL.Charting;

public partial class PieChart : BaseChartComponent
{
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Options.Legend = new Legend
        {
            FontSize = "14px",
            FontWeight = "bold",
            Position = LegendPosition.Bottom,
            Labels = new LegendLabels
            {
                Colors = new Color("#9D9D9D")
            }
        };
    }
}