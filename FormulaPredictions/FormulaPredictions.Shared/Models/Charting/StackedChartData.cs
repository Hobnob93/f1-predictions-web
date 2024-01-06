namespace FormulaPredictions.Shared.Models.Charting;

public class StackedChartData
{
    public string TopStackName { get; set; } = string.Empty;
    public string BottomStackName { get; set; } = string.Empty;

    public List<ChartDataPoint> TopStackData { get; set; } = [];
    public List<ChartDataPoint> BottomStackData { get; set; } = [];
}
