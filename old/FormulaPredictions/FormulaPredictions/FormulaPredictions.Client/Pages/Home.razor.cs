using FormulaPredictions.Shared.State;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.Client.Pages;

public partial class Home : ComponentBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    [CascadingParameter]
    private AppData AppData { get; set; } = default!;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (AppData is null)
            return;

        if (!AppData.HasFetched)
            NavigationManager.NavigateTo("./fetch-data");
    }
}