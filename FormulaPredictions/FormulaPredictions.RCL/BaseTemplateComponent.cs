using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.RCL.State;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.Models.Base;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL;

public abstract class BaseTemplateComponent : ComponentBase
{
    [Inject]
    protected IResponsesService ResponsesService { get; set; } = default!;

    [CascadingParameter]
    protected CascadingState AppState { get; set; } = default!;

    [Parameter]
    public Competitor Competitor { get; set; } = default!;

    protected int Year => AppState.AppData.Config.Year;

    protected T GetResponseForCompetitor<T>() where T : BaseItem
    {
        return ResponsesService.GetSingleResponse<T>(Competitor.Id, AppState.AppData, AppState.Current!);
    }
}
