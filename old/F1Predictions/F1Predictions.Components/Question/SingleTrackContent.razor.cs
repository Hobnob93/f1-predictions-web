using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using F1Predictions.Core.Services;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Question
{
    public partial class SingleTrackContent : QuestionContent
    {
        [Inject]
        private ICompResponses<Track> Responses { get; set; } = default!;

        protected override void SetResponses()
        {
            var tracks = Responses.GetAllResponses();
            var uniqueTracks = tracks
                .GroupBy(t => t.Id)
                .Select(g => g.First())
                .ToList();

            ResponseData = uniqueTracks.Select(ut => new ChartDataPoint
            {
                Id = ut.Id,
                Name = ut.Name,
                Color = ut.Color,
                Value = tracks.Count(t => t.Id == ut.Id)
            }).ToList();
        }
    }
}
