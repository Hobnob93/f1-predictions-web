using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.RCL.State;
using FormulaPredictions.Shared.Models.Base;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Templates.Competitor;

public abstract class CompetitorTemplateComponent : OneTimeRenderComponent
{
    [Inject]
    protected IResponsesService ResponsesService { get; set; } = default!;

    [CascadingParameter]
    protected CascadingState AppState { get; set; } = default!;

    [Parameter]
    public Shared.Models.Competitor Competitor { get; set; } = default!;

    protected int Year => AppState.AppData.Config.Year;

    protected T GetResponseForCompetitor<T>() where T : BaseItem
    {
        return ResponsesService.GetSingleResponse<T>(Competitor.Id, AppState.AppData, AppState.Current!);
    }

    protected T[] GetResponsesForCompetitor<T>() where T : BaseItem
    {
        return ResponsesService.GetAllResponses<T>(Competitor.Id, AppState.AppData, AppState.Current!);
    }

    protected T GetResponseValueForCompetitor<T>() where T : struct
    {
        return ResponsesService.GetValueResponse<T>(Competitor.Id, AppState.Current!);
    }
}
