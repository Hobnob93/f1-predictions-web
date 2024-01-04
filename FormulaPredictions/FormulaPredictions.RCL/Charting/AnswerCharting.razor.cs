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

    protected List<ChartDataPoint> ChartData { get; set; } = [];

    protected override void OnInitialized()
    {
        base.OnInitialized();

        var answerItems = GetAnswerItems(Answer.ScoringMode);
        ChartData = Answer.AnswersData
            .Select(ad => (Data: ad, Item: answerItems.Single(ai => string.Equals(ai.Id, ad.Id, StringComparison.OrdinalIgnoreCase))))
            .Select((d, i) => new ChartDataPoint
            {
                Id = d.Item.Id,
                Index = i,
                Color = d.Item.Color,
                Name = d.Item.Name,
                Value = decimal.Parse(d.Data.Value)
            }).ToList();
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
}