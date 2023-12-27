using FormulaPredictions.RCL.State;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.Client.Pages;

public partial class Home : ComponentBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    [CascadingParameter]
    private CascadingState AppState { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (!AppState.AppData.HasFetched)
            NavigationManager.NavigateTo("./fetch-data");

        AppState.AppBarText = "The Results";
    }
}