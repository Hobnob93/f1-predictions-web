namespace F1Predictions.Core.Models
{
    public class StackedChartData
    {
        public string LeftStackName { get; set; } = string.Empty;
        public string RightStackName { get; set; } = string.Empty;

        public List<ChartDataPoint> LeftStackData { get; set; } = new();
        public List<ChartDataPoint> RightStackData { get; set; } = new();
    }
}
