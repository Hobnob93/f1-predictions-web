using FormulaPredictions.RCL.State;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Questions;

public partial class QuestionContent : ComponentBase
{
    [CascadingParameter]
    private CascadingState AppState { get; set; } = default!;

    private Type? CompetitorTemplate => AppState.Current?.CompetitorResponseTemplate;

    private Dictionary<string, object> GetTemplateParameters(Competitor competitor)
    {
        return new() { { "Competitor", competitor } };
    }
}