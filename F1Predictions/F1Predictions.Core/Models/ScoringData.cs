using F1Predictions.Core.Enums;

namespace F1Predictions.Core.Models
{
    public class ScoringData
    {
        public ScoringType Type { get; set; } = ScoringType.None;
        public string AnswersId { get; set; } = string.Empty;
        public int? Index { get; set; }
        public int Value { get; set; }
    }
}
