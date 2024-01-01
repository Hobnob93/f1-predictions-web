using FormulaPredictions.RCL.Templates.Charting;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Charting;

public partial class AnswerCharting : BaseRclComponent
{
    [Parameter, EditorRequired]
    public Answer Answer { get; set; } = default!;

    [Parameter, EditorRequired]
    public Type RenderType { get; set; } = default!;

    private Dictionary<string, object> GetTemplateParameters()
    {
        return new() { { nameof(ChartingTemplateComponent.Answer), Answer } };
    }
}