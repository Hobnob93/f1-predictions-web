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
        public List<ChartDataPoint> ChartData { get; protected set; } = new();
        public List<ListDataPoint> ListData { get; protected set; } = new();
        public StackedChartData StackedChartData { get; protected set; } = new();


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
                case RenderType.List:
                    DetermineListData();
                    break;
                case RenderType.Paired:
                    DetermineStackedChartData();
                    break;
            }
        }

        private void DetermineListData()
        {
            var rawItemIds = Answer.RawAnswer?.Split(",") ?? Enumerable.Empty<string>();

            ListData = (Answer.Mode switch
            {
                ScoringMode.Teams => rawItemIds
                    .Select(id => Teams.FindItem(id))
                    .Select(team => new ListDataPoint
                    {
                        Id = team.Id,
                        Color = team.Color,
                        Name = team.Name
                    }),
                ScoringMode.Drivers => rawItemIds
                    .Select(id => Drivers.FindItem(id))
                    .Select(driver => new ListDataPoint
                    {
                        Id = driver.Id,
                        Color = driver.Color,
                        Name = $"{driver.FirstName} {driver.LastName}"
                    }),
                _ => Enumerable.Empty<ListDataPoint>()
            }).ToList();
        }

        private void DetermineChartData()
        {
            ChartData = (Answer.Mode switch
            {
                ScoringMode.Teams => Answer.AnswersData
                    .Select(ad => (Data: ad, Team: Teams.FindItem(ad.Id)))
                    .Select((item, index) => new ChartDataPoint
                    {
                        Id = item.Team.Id,
                        Index = index,
                        Color = item.Team.Color,
                        Name = item.Team.Name,
                        Value = decimal.Parse(item.Data.Value)
                    }),
                ScoringMode.Drivers => Answer.AnswersData
                    .Select(ad => (Data: ad, Driver: Drivers.FindItem(ad.Id)))
                    .Select((item, index) => new ChartDataPoint
                    {
                        Id = item.Driver.Id,
                        Index = index,
                        Color = item.Driver.Color,
                        Name = item.Driver.LastName,
                        Value = decimal.Parse(item.Data.Value)
                    }),
                ScoringMode.Tracks => Answer.AnswersData
                    .Select(ad => (Data: ad, Track: Tracks.FindItem(ad.Id)))
                    .Select((item, index) => new ChartDataPoint
                    {
                        Id = item.Track.Id,
                        Index = index,
                        Color = item.Track.Color,
                        Name = item.Track.Name,
                        Value = decimal.Parse(item.Data.Value)
                    }),
                _ => Enumerable.Empty<ChartDataPoint>()
            }).ToList();
        }

        private void DetermineStackedChartData()
        {
            DetermineChartData();

            var topDistinct = ChartData.DistinctBy(cd => cd.Id).First();
            var leftSection = ChartData.Where(cd => cd.Id == topDistinct.Id);
            var leftSectionName = leftSection.First().Name;
            var rightSection = ChartData.Where(cd => cd.Id != topDistinct.Id);
            var rightSectionName = rightSection.First().Name;

            leftSection.First().Name = "Quali";
            rightSection.First().Name = "Quali";
            leftSection.Last().Name = "Race";
            rightSection.Last().Name = "Race";

            StackedChartData = new()
            {
                LeftStackName = leftSectionName,
                RightStackName = rightSectionName,
                LeftStackData = leftSection.ToList(),
                RightStackData = rightSection.ToList()
            };
        }
    }
}
