using BlazorComponentUtilities;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Question
{
    public partial class MultiDriverTrackContent : QuestionContent
    {
        [Inject]
        private IDriversDataService DriversService { get; set; } = default!;

        [Inject]
        private ITracksDataService TracksServicve { get; set; } = default!;

        protected override void SetResponses()
        {
            //var driverIds = AnswerService.GetAnswersRaw()
            //    .SelectMany(a => a.Split(","));
            //
            //var drivers = driverIds
            //    .Distinct()
            //    .Select(id => DriversService.FindItem(id))
            //    .ToList();
            //
            //ChartOptions.ChartPalette = drivers
            //    .Select(t => t.Color)
            //    .ToArray();
            //
            //ResponseData = drivers.Select(t => new ChartDataPoint
            //{
            //    Id = t.Id,
            //    Name = t.LastName,
            //    Color = t.Color,
            //    Value = driverIds.Count(id => id == t.Id)
            //}).ToList();
        }

        private List<(string DriverId, string TrackId)> GetCompetitorAnswers(string competitorId)
        {
            var answerIds = AnswerService.GetCompetitorAnswerRaw(competitorId)
                .Split(",")
                .Select(s => (s.Split("-").First(), s.Split("-").Last()))
                .ToList();

            return answerIds;
        }

        private string GetDriverTrackContainerClasses(bool compIsRightAligned)
        {
            return new CssBuilder()
                .AddClass("d-flex")
                .AddClass("flex-row", when: !compIsRightAligned)
                .AddClass("flex-row-reverse", when: compIsRightAligned)
                .AddClass("align-center")
                .Build();
        }

        private string GetDriverClasses(bool compIsRightAligned)
        {
            return new CssBuilder()
                .AddClass("ms-n4", when: !compIsRightAligned)
                .AddClass("me-n4-5", when: compIsRightAligned)
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
