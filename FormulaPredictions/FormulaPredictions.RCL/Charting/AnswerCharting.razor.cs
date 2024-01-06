using FormulaPredictions.Shared.Enums;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.Models.Base;
using FormulaPredictions.Shared.Models.Charting;
using FormulaPredictions.Shared.State;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Charting;

public partial class AnswerCharting : BaseRclComponent
{
    [Parameter, EditorRequired]
    public Answer Answer { get; set; } = default!;

    [Parameter, EditorRequired]
    public AppData AppData { get; set; } = default!;

    private string Title => IsSingleAnswer() ? "Actual" : "Actual";

    private List<ChartDataPoint> GetAnswerChartData()
    {
        var answerItems = GetAnswerItems(Answer.ScoringMode);
        return Answer.AnswersData
            .Select(ad => (Data: ad, Item: answerItems.Single(ai => string.Equals(ai.Id, ad.Id, StringComparison.OrdinalIgnoreCase))))
            .Select((d, i) => new ChartDataPoint
            {
                Id = d.Item.Id,
                Index = i,
                Color = d.Item.Color,
                Name = d.Item.Name,
                Value = decimal.Parse(d.Data.Value)
            })
            .OrderByDescending(c => c.Value)
            .ToList();
    }

    private BaseColorItem[] GetAnswerItems(ScoringMode scoringMode)
    {
        return scoringMode switch
        {
            ScoringMode.Teams => AppData.GetDataArray<Team>().Cast<BaseColorItem>().ToArray(),
            ScoringMode.Drivers => AppData.GetDataArray<Driver>().Cast<BaseColorItem>().ToArray(),
            ScoringMode.Tracks => AppData.GetDataArray<Circuit>().Cast<BaseColorItem>().ToArray(),
            _ => throw new NotImplementedException($"{scoringMode} not implemented")
        };
    }

    private MultiBarChartData GetMultiAnswerChartData()
    {
        var data = new MultiBarChartData();
        if (Answer.ScoringMode != ScoringMode.DriverTracks)
            return data;

        var tracks = GetAnswerItems(ScoringMode.Tracks);
        var trackAnswers = Answer.AnswersData
            .Select(ad => tracks.Single(t => t.Id == ad.Id))
            .ToArray();

        var drivers = GetAnswerItems(ScoringMode.Drivers);
        var driverScoreGroups = Answer.AnswersData
            .SelectMany(ad => ad.Value.Split(','))
            .Select(str => (DriverId: str.Split('-').First(), RawScore: str.Split('-').Last()))
            .Select(data => (Driver: drivers.Single(d => d.Id == data.DriverId), Score: int.Parse(data.RawScore)))
            .GroupBy(data => data.Driver.Id);

        foreach (var group in driverScoreGroups)
        {
            var driver = drivers.Single(d => d.Id == group.Key);
            List<MultiBarChartDataPoint> dataPoints = [];

            var index = 0;
            foreach (var item in group)
            {
                var track = trackAnswers[index];
                dataPoints.Add(new MultiBarChartDataPoint
                {
                    Id = $"{track.Id}-{driver.Id}",
                    Color = driver.Color,
                    Name = driver.Name,
                    Value = item.Score,
                    XValue = track.Name
                });

                index++;
            }

            data.DataPoints.Add(dataPoints);
        }

        return data;
    }

    private List<ChartDataPoint> GetSingleAnswerDataPoint()
    {
        var answer = int.Parse(Answer.RawAnswer ?? throw new InvalidCastException("String is null!"));

        return 
        [
            new ChartDataPoint
            {
                Id = Answer.RawAnswer,
                Index = 0,
                Color = "#2ab546",
                Name = Answer.RawAnswer,
                Value = answer
            }
        ];
    }

    private StackedChartData GetStackedChartData()
    {
        var answerItems = GetAnswerItems(Answer.ScoringMode);
        var chartData = Answer.AnswersData
            .Select(ad => (Data: ad, Item: answerItems.Single(ai => string.Equals(ai.Id, ad.Id, StringComparison.OrdinalIgnoreCase))))
            .Select((d, i) => new ChartDataPoint
            {
                Id = d.Item.Id,
                Index = i,
                Color = d.Item.Color,
                Name = d.Item.Name,
                Value = decimal.Parse(d.Data.Value)
            })
            .ToList();

        var firstDisctinct = chartData.DistinctBy(cd => cd.Id).First();
        var topSection = chartData.Where(cd => cd.Id == firstDisctinct.Id);
        var topSectionName = topSection.First().Name;
        var bottomSection = chartData.Where(cd => cd.Id != firstDisctinct.Id);
        var bottomSectionName = bottomSection.First().Name;

        topSection.First().Name = "Quali";
        bottomSection.First().Name = "Quali";
        topSection.Last().Name = "Race";
        bottomSection.Last().Name = "Race";

        return new()
        {
            TopStackName = topSectionName,
            BottomStackName = bottomSectionName,
            TopStackData = topSection.ToList(),
            BottomStackData = bottomSection.ToList()
        };
    }

    private bool IsNonNumericRenderType()
    {
        return Answer.RenderType switch
        {
            RenderType.Raw => true,
            RenderType.Bool => true,
            RenderType.List => true,
            RenderType.OrderedList => true,
            _ => false
        };
    }

    private bool IsSingleAnswer()
    {
        return Answer.RenderType switch
        {
            RenderType.Raw => true,
            RenderType.Bool => true,
            _ => false
        };
    }
}