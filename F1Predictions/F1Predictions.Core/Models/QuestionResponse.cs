namespace F1Predictions.Core.Models
{
    public class QuestionResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Question { get; set; } = string.Empty;
        public string Scoring { get; set; } = string.Empty;
        public object KH { get; set; } = default!;
        public object HI { get; set; } = default!;
        public object SH { get; set; } = default!;
        public object KC { get; set; } = default!;
        public object TW { get; set; } = default!;
        public object GH { get; set; } = default!;
        public object MG { get; set; } = default!;
        public object KB { get; set; } = default!;
        public object CB { get; set; } = default!;
        public object MH { get; set; } = default!;
    }
}
