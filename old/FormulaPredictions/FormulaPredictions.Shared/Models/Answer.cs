using FormulaPredictions.Shared.Enums;
using FormulaPredictions.Shared.Models.Base;

namespace FormulaPredictions.Shared.Models;

public class Answer : BaseItem
{
    public RenderType RenderType { get; set; }
    public ScoringMode ScoringMode { get; set; }
    public string? RawAnswer { get; set; }
    public AnswerData[] AnswersData { get; set; } = [];
}
