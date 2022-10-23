using F1Predictions.Core.Interfaces;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Pages
{
    public partial class Index
    {
        [Inject]
        public IDataServicesInitializer DataInitializer { get; set; } = default!;

        [Inject]
        public NavigationManager NavManager { get; set; } = default!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            await DataInitializer.InitializeCompetitorsAsync();
            await DataInitializer.InitializeTeamsAsync();

            NavManager.NavigateTo("/results");
        }
    }
}
