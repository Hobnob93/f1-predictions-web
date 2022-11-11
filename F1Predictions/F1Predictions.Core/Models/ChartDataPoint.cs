namespace F1Predictions.Core.Models
{
    public class ChartDataPoint
    {
        public int Index { get; set; }
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = "#CFCFCF";
        public decimal Value { get; set; }
    }
}
