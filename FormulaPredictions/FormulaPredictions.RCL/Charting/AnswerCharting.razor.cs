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

    private string Title => IsSingleAnswer() ? "Answer" : "Answers";

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