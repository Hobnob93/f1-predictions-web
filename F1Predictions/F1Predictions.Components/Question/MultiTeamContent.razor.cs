using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace F1Predictions.Components.Question
{
    public partial class MultiTeamContent : QuestionContent
    {
        [Inject]
        private ITeamsDataService TeamsService { get; set; } = default!;

        protected override void SetResponses()
        {
            var teamIds = AnswerService.GetAnswersRaw()
                .SelectMany(a => a.Split(","));

            var teams = teamIds
                .Distinct()
                .Select(id => TeamsService.FindItem(id))
                .ToList();

            ChartOptions.ChartPalette = teams
                .Select(t => t.Color)
                .ToArray();

            ResponseData = teams.Select(t => new ChartDataPoint
            {
                Id = t.Id,
                Name = t.Name,
                Value = teamIds.Count(id => id == t.Id)
            }).ToList();
        }

        private List<string> GetCompetitorAnswers(string competitorId)
        {
            var answerIds = AnswerService.GetCompetitorAnswerRaw(competitorId)
                .Split(",")
                .ToList();

            return answerIds;
        }
    }
}
