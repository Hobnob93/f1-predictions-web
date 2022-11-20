using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Question
{
    public partial class MultiTrackContent : QuestionContent
    {
        [Inject]
        private ITracksDataService TracksService { get; set; } = default!;

        protected override void SetResponses()
        {
            var trackIds = AnswerService.GetAllRawResponses()
                .SelectMany(a => a.Split(","));

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

        private List<string> GetCompetitorAnswers(string competitorId)
        {
            var answerIds = AnswerService.GetRawResponseForComp(competitorId)
                .Split(",")
                .ToList();

            return answerIds;
        }
    }
}
