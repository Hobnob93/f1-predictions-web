using FormulaPredictions.Shared.Enums;
using FormulaPredictions.Shared.Models.Base;

namespace FormulaPredictions.Shared.Models;

public class QuestionResponses : BaseItem
{
    public string Question { get; set; } = string.Empty;
    public QuestionType Type { get; set; }
    public ScoringData Scoring { get; set; } = new();
    public Note[] Stars { get; set; } = [];
    public CompetitorResponse[] CompetitorResponses { get; set; } = [];
}
