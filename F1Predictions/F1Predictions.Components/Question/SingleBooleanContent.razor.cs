using F1Predictions.Core.Models;

namespace F1Predictions.Components.Question
{
    public partial class SingleBooleanContent : QuestionContent
    {
        protected override void SetResponses()
        {
            var answers = AnswerService.GetAnswersRaw();
            var uniqueAnswers = answers
                .Distinct()
                .Select(id => (string.Equals(id, "true", StringComparison.OrdinalIgnoreCase), id))
                .ToList();

            ResponseData = uniqueAnswers.Select(ua => new ChartDataPoint
            {
                Id = ua.id,
                Name = ua.Item1 ? "True" : "False",
                Color = ua.Item1 ? "#78FF3c" : "#FF483c",
                Value = answers.Count(a => a == ua.id)
            }).ToList();
        }

        private bool GetCompetitorAnswer(string competitorId)
        {
            var rawAnswer = AnswerService.GetCompetitorAnswerRaw(competitorId);
            return string.Equals(rawAnswer, "true", StringComparison.OrdinalIgnoreCase);
        }
    }
}
