using BlazorComponentUtilities;
using FormulaPredictions.RCL.State;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Questions;

public partial class QuestionText : BaseRclComponent
{
    [CascadingParameter]
    private CascadingState AppState { get; set; } = default!;

    private QuestionResponses? CurrentQuestion => AppState.Current?.Question;
    private string Text => CurrentQuestion?.Question ?? string.Empty;
    private string? Scoring => CurrentQuestion?.Scoring.Explanation;

    public string Classes => new CssBuilder()
        .AddClass("mt-n4")
        .AddClass(Class, when: Class is not null)
        .Build();
}