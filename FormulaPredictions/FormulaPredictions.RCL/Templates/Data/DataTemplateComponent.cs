using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.RCL.State;
using FormulaPredictions.Shared.Models.Base;
using FormulaPredictions.Shared.Models.Charting;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Templates.Data;

public abstract class DataTemplateComponent : OneTimeRenderComponent
{
    [Inject]
    protected IResponsesService ResponsesService { get; set; } = default!;

    [Inject]
    protected IAnswersService AnswersService { get; set; } = default!;

    [CascadingParameter]
    protected CascadingState AppState { get; set; } = default!;

    protected List<ChartDataPoint> ResponseData { get; set; } = [];

    protected virtual void OnSelectedChartDataPoint(ChartDataPoint dataPoint)
    {
        if (AppState.Current is null)
            return;

        var currentData = AppState.Current with { };
        foreach (var competitor in dataPoint.ApplicableCompetitors)
        {
            if (!currentData.ShowingCompetitorResponses.Contains(competitor))
                currentData.ShowingCompetitorResponses.Add(competitor);
        }

        AppState.Current = currentData with
        {
            OpenGraphSection = false
        };
    }

    protected RawCompetitorResponse<T>[] GetResponsesForAllCompetitors<T>() where T : BaseItem
    {
        return ResponsesService.GetAllResponses<T>(AppState.AppData, AppState.Current!);
    }
}
