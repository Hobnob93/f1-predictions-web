using FormulaPredictions.Shared.Enums;

namespace FormulaPredictions.Shared.Models;

public class ScoringData
{
    public ScoringType Type { get; set; }
    public string AnswersId { get; set; } = string.Empty;
    public string? Explanation { get; set; }
    public int? Index { get; set; }
    public int Value { get; set; }
    public int ExtraValue { get; set; }
}
