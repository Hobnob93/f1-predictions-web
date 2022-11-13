using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Question
{
    public partial class MultiTrackContent : QuestionContent
    {
        [Inject]
        private ITracksDataService TracksService { get; set; } = default!;

        [Inject]
        private ITeamsDataService TeamsService { get; set; } = default!;

        protected override void SetResponses()
        {
            var trackIds = AnswerService.GetAnswersRaw()
                .SelectMany(a => a.Split(","));

            var tracks = trackIds
                .Distinct()
                .Select(id => TracksService.FindItem(id))
                .ToList();

            var random = new Random();
            var colors = TeamsService.Data.Select(d => d.Color)
                .OrderBy(d => random.Next())
                .ToList();

            ResponseData = tracks.Select((t, i) => new ChartDataPoint
            {
                Id = t.Id,
                Name = t.Name,
                Color = colors[i],
                Value = trackIds.Count(id => id == t.Id)
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
