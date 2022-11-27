namespace F1Predictions.Core.Models
{
    public class Answer : DataItem
    {
        public string? RawAnswer { get; set; }
        public List<AnswerData> AnswersData { get; set; } = new();
    }
}
