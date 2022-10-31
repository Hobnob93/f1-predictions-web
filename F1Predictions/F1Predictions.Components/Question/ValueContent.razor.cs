using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace F1Predictions.Components.Question
{
    public partial class ValueContent : QuestionContent
    {
        protected override void SetResponses()
        {
            base.SetResponses();

            var answers = AnswerService
                .GetAnswersRaw()
                .Select(a => int.Parse(a))
                .OrderBy(a => a);

            var uniqueAnswers = answers.Distinct();

            ResponseData = uniqueAnswers.Select(ua => new ChartDataPoint
            {
                Id = ua.ToString(),
                Name = ua.ToString(),
                Value = answers.Count(a => ua == a)
            }).ToList();
        }

        private string GetCompetitorAnswer(string competitorId)
        {
            return AnswerService.GetCompetitorAnswerRaw(competitorId);
        }
    }
}
