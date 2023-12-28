using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Responses;

public partial class BooleanResponse : BaseRclComponent
{
    [Parameter, EditorRequired]
    public bool State { get; set; }
}