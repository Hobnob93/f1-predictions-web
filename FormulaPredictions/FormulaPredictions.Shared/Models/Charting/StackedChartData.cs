namespace FormulaPredictions.Shared.Models.Charting;

public class StackedChartData
{
    public string LeftStackName { get; set; } = string.Empty;
    public string RightStackName { get; set; } = string.Empty;

    public List<ChartDataPoint> LeftStackData { get; set; } = [];
    public List<ChartDataPoint> RightStackData { get; set; } = [];
}
