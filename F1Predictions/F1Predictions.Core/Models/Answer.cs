using F1Predictions.Core.Enums;

namespace F1Predictions.Core.Models
{
    public class Answer : DataItem
    {
        public RenderType RenderType { get; set; }
        public ScoringMode Mode { get; set; }
        public string? RawAnswer { get; set; }
        public List<AnswerData> AnswersData { get; set; } = new();
    }
}
