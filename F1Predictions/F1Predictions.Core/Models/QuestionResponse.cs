using F1Predictions.Core.Enums;

namespace F1Predictions.Core.Models
{
    public class QuestionResponse : DataItem
    {
        public string Question { get; set; } = string.Empty;
        public QuestionType Type { get; set; } = QuestionType.None;
        public ScoringType Scoring { get; set; } = ScoringType.None;
        public string KH { get; set; } = string.Empty;
        public string HI { get; set; } = string.Empty;
        public string SH { get; set; } = string.Empty;
        public string KC { get; set; } = string.Empty;
        public string TW { get; set; } = string.Empty;
        public string GH { get; set; } = string.Empty;
        public string MG { get; set; } = string.Empty;
        public string KB { get; set; } = string.Empty;
        public string CB { get; set; } = string.Empty;
        public string MH { get; set; } = string.Empty;
    }
}
