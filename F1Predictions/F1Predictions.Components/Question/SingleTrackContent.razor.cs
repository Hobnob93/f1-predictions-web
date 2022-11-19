using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using F1Predictions.Core.Services;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Question
{
    public partial class SingleTrackContent : QuestionContent
    {
        [Inject]
        private ITracksDataService TracksService { get; set; } = default!;

        protected override void SetResponses()
        {
            var trackIds = AnswerService.GetAnswersRaw();
            var tracks = trackIds
                .Distinct()
                .Select(id => TracksService.FindItem(id))
                .ToList();

            ResponseData = tracks.Select(t => new ChartDataPoint
            {
                Id = t.Id,
                Name = t.Name,
                Color = t.Color,
                Value = trackIds.Count(id => id == t.Id)
            }).ToList();
        }

        private string GetCompetitorAnswerId(string competitorId)
        {
            return AnswerService.GetCompetitorAnswerRaw(competitorId);
        }
    }
}
