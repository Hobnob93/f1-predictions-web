using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Charting;

public partial class AnswerCharting : BaseRclComponent
{
    [Parameter, EditorRequired]
    public Answer Answer { get; set; } = default!;
}