using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Charting;

public partial class ChartContainer : ComponentBase
{
    [Parameter, EditorRequired]
    public string Title { get; set; } = string.Empty;

    [Parameter, EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;
}