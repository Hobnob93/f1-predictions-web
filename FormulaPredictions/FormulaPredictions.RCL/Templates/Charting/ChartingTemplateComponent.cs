using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Templates.Charting;

public abstract class ChartingTemplateComponent : OneTimeRenderComponent
{
    [Parameter, EditorRequired]
    public Answer Answer { get; set; } = default!;
}
