using FormulaPredictions.RCL.State;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Core;

public partial class SectionBar
{
    [CascadingParameter]
    private CascadingState AppState { get; set; } = default!;

    private string SeasonText => $"{AppState.AppData.Config.Year} Season";
}
