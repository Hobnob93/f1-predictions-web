using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Question
{
    public partial class ValueContent : QuestionContent
    {
        [Inject]
        private ITeamsDataService TeamsService { get; set; } = default!;

        protected override void SetResponses()
        {
            var answers = CompResponses
                .GetAllRawResponses()
                .Select(a => int.Parse(a))
                .OrderBy(a => a);

            var uniqueAnswers = answers.Distinct();

            var random = new Random();
            var colors = TeamsService.Data.Select(d => d.Color)
                .OrderBy(d => random.Next())
                .ToList();

            ResponseData = uniqueAnswers.Select((ua, i) => new ChartDataPoint
            {
                Index = i,
                Id = ua.ToString(),
                Name = ua.ToString(),
                Color = colors[i],
                Value = answers.Count(a => ua == a)
            }).ToList();
        }

        private string GetCompetitorAnswer(string competitorId)
        {
            return CompResponses.GetRawResponseForComp(competitorId);
        }
    }
}
