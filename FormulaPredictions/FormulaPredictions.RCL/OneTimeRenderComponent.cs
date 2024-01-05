using FormulaPredictions.RCL.State;
using FormulaPredictions.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL;

public abstract class OneTimeRenderComponent : ComponentBase
{
    [CascadingParameter]
    protected CascadingState AppState { get; set; } = default!;

    private string? _previousQuestionId = null;
    private QuestionType? _previousQuestionType = null;

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        _previousQuestionType = AppState.Current?.Question.Type;
        _previousQuestionId = AppState.Current?.Question.Id;
    }

    protected override bool ShouldRender()
    {
        if (_previousQuestionType is null)
            return true;

        return
            _previousQuestionType == AppState.Current?.Question.Type &&
            _previousQuestionId != AppState.Current?.Question.Id;
    }
}
