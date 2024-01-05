using FormulaPredictions.Shared.Enums;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.Models.Base;
using FormulaPredictions.Shared.Models.Charting;
using FormulaPredictions.Shared.State;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Charting;

public partial class AnswerListing : BaseRclComponent
{
    [Parameter, EditorRequired]
    public Answer Answer { get; set; } = default!;

    [Parameter, EditorRequired]
    public AppData AppData { get; set; } = default!;

    private List<ListDataPoint> ListData => GetAnswerListData();

    private List<ListDataPoint> GetAnswerListData()
    {
        var answerItems = GetAnswerItems(Answer.ScoringMode);
        return (Answer.RawAnswer?.Split(',') ?? [])
            .Select(a => answerItems.Single(ai => string.Equals(ai.Id, a, StringComparison.OrdinalIgnoreCase)))
            .Select(ai => new ListDataPoint
            {
                Id = ai.Id,
                Color = ai.Color,
                Name = ai.Name
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