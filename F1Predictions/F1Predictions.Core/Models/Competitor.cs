namespace F1Predictions.Core.Models
{
    public class Competitor : DataItem
    {
        public string Name { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public bool UseDarkText { get; set; } = true;
        public bool IsShowingContent { get; set; }

        public int Index { get; set; }
        public bool IsRightAligned => Index % 2 == 1;
    }
}
