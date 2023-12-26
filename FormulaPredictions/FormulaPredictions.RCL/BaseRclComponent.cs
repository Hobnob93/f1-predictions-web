using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL;

public abstract class BaseRclComponent : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> UserAttributes { get; set; } = [];

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }
}
