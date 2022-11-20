using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace F1Predictions.Components.Question
{
    public partial class IntroContent : QuestionContent
    {
        [Inject]
        public ITeamsDataService TeamsService { get; set; } = default!;

        protected override void SetResponses()
        {
            var liveryIds = AnswerService.GetAllRawResponses();
            var teamsFromLiveries = liveryIds
                .Distinct()
                .Select(id => TeamsService.FindItem(id))
                .ToList();

            ResponseData = teamsFromLiveries.Select(t => new ChartDataPoint
            {
                Id = t.Id,
                Name = t.Name,
                Color = t.Color,
                Value = liveryIds.Count(id => id == t.Id)
            }).ToList();
        }
    }
}
