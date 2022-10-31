namespace F1Predictions.Core.Models
{
    public class Competitor
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public bool UseDarkText { get; set; } = true;
        public bool IsShowingContent { get; set; }
    }
}
