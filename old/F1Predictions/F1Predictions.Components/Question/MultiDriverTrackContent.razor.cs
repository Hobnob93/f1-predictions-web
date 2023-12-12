using BlazorComponentUtilities;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Question
{
    public partial class MultiDriverTrackContent : QuestionContent
    {
        [Inject]
        private IMultiCompResponses<DriverTrack> Responses { get; set; } = default!;

        protected override void SetResponses()
        {
            var tracks = Responses.GetAllMultiResponses()
                .SelectMany(r => r.Select(rr => rr.Track))
                .ToList();

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

        private string GetDriverTrackContainerClasses(bool compIsRightAligned)
        {
            return new CssBuilder()
                .AddClass("d-flex")
                .AddClass("flex-row", when: !compIsRightAligned)
                .AddClass("flex-row-reverse", when: compIsRightAligned)
                .AddClass("align-end")
                .Build();
        }

        private string GetDriverClasses(bool compIsRightAligned)
        {
            return new CssBuilder()
                .AddClass("ms-n4", when: !compIsRightAligned)
                .AddClass("me-n4", when: compIsRightAligned)
                .Build();
        }

        private string GetTrackClasses(bool compIsRightAligned)
        {
            return new CssBuilder()
                .AddClass("ms-n12", when: !compIsRightAligned)
                .AddClass("me-n9", when: compIsRightAligned)
                .Build();
        }
    }
}
