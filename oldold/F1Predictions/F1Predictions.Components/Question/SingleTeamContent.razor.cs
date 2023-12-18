using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Question
{
    public partial class SingleTeamContent : QuestionContent
    {
        [Inject]
        private ICompResponses<Team> Responses { get; set; } = default!;

        protected override void SetResponses()
        {
            var teams = Responses.GetAllResponses();
            var uniqueTeams = teams
                .GroupBy(t => t.Id)
                .Select(g => g.First())
                .ToList();

            ResponseData = uniqueTeams.Select(ut => new ChartDataPoint
            {
                Id = ut.Id,
                Name = ut.Name,
                Color = ut.Color,
                Value = teams.Count(t => t.Id == ut.Id)
            }).ToList();
        }
    }
}
