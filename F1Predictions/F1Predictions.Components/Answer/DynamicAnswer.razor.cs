using F1Predictions.Core.Enums;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Answer
{
    public partial class DynamicAnswer : IRefreshable
    {
        [Inject]
        private IQuestionsDataService Questions { get; set; } = default!;

        [Inject]
        private IAnswersDataService Answers { get; set; } = default!;

        [Inject]
        private IDriversDataService Drivers { get; set; } = default!;

        [Inject]
        private ITeamsDataService Teams { get; set; } = default!;

        [Inject]
        private ITracksDataService Tracks { get; set; } = default!;

        public F1Predictions.Core.Models.Answer Answer { get; set; } = default!;
        public IRefreshable? AnswersChart { get; set; }
        public List<ChartDataPoint> AnswerData { get; protected set; } = new();


        protected override void OnInitialized()
        {
            Initialize();
        }

        public async Task Refresh()
        {
            Initialize();

            await InvokeAsync(StateHasChanged);

            if (AnswersChart is not null)
                await AnswersChart.Refresh();
        }

        private void Initialize()
        {
            var currentQuestion = Questions.CurrentQuestion;
            var answerId = currentQuestion.Scoring.AnswersId;

            Answer = Answers.FindItem(answerId);

            switch (Answer.RenderType)
            {
                case RenderType.Pie:
                    DetermineChartData();
                    break;
                case RenderType.Bar when Answer.Mode != ScoringMode.DriverTracks:
                    DetermineChartData();
                    break;
            }
        }

        private void DetermineChartData()
        {
            AnswerData = (Answer.Mode switch
            {
                ScoringMode.Teams => Answer.AnswersData
                    .Select(ad => (ad, Teams.FindItem(ad.Id)))
                    .Select((item, index) => new ChartDataPoint
                    {
                        Id = item.Item2.Id,
                        Index = index,
                        Color = item.Item2.Color,
                        Name = item.Item2.Name,
                        Value = decimal.Parse(item.ad.Value)
                    }),
                ScoringMode.Drivers => Answer.AnswersData
                    .Select(ad => (ad, Drivers.FindItem(ad.Id)))
                    .Select((item, index) => new ChartDataPoint
                    {
                        Id = item.Item2.Id,
                        Index = index,
                        Color = item.Item2.Color,
                        Name = item.Item2.LastName,
                        Value = decimal.Parse(item.ad.Value)
                    }),
                ScoringMode.Tracks => Answer.AnswersData
                    .Select(ad => (ad, Tracks.FindItem(ad.Id)))
                    .Select((item, index) => new ChartDataPoint
                    {
                        Id = item.Item2.Id,
                        Index = index,
                        Color = item.Item2.Color,
                        Name = item.Item2.Name,
                        Value = decimal.Parse(item.ad.Value)
                    }),
                _ => Enumerable.Empty<ChartDataPoint>()
            }).ToList();
        }
    }
}
