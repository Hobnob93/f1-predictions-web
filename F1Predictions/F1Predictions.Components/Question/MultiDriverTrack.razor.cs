using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Question
{
    public partial class MultiDriverTrack : QuestionContent
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
    }
}
