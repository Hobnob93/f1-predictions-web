using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace F1Predictions.Components.Question
{
    public partial class MultiDriverContent : QuestionContent
    {
        [Inject]
        private IDriversDataService DriversService { get; set; } = default!;

        protected override void SetResponses()
        {
            var driverIds = AnswerService.GetAnswersRaw()
                .SelectMany(a => a.Split(","));

            var drivers = driverIds
                .Distinct()
                .Select(id => DriversService.FindItem(id))
                .ToList();

            ChartOptions.ChartPalette = drivers
                .Select(t => t.Color)
                .ToArray();

            ResponseData = drivers.Select(t => new ChartDataPoint
            {
                Id = t.Id,
                Name = t.LastName,
                Value = driverIds.Count(id => id == t.Id)
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
