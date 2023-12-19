using FormulaPredictions.Shared.State;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.State;

public partial class CascadingState : ComponentBase
{
    [Parameter, EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;

    private AppData _appData = AppData.CreateDefault();
    public AppData AppData
    {
        get => _appData;
        set
        {
            if (_appData != value)
            {
                _appData = value;
                StateHasChanged();
            }
        }
    }
}