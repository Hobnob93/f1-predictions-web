using FormulaPredictions.Shared.State;
using Microsoft.AspNetCore.Components;
using FormulaPredictions.Shared.Constants;
using FormulaPredictions.Shared.Services.Interfaces;

namespace FormulaPredictions.Components.State;

public partial class CascadingState : ComponentBase, IDisposable
{
    [Inject]
    private IPredictionsData PredictionsData { get; set; } = default!;

    [Inject]
    private PersistentComponentState ApplicationState { get; set; } = default!;

    [Parameter, EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;

    private PersistingComponentStateSubscription _persistingSubscription;

    private AppData? _appData;
    public AppData? AppData
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

    protected override async Task OnInitializedAsync()
    {
        _persistingSubscription =
            ApplicationState.RegisterOnPersisting(PersistData);

        if (!ApplicationState.TryTakeFromJson<AppData>(CascadingConstants.AppStateValueName, out var restored))
        {
            AppData = await FetchAllAppData();
        }
        else
        {
            AppData = restored!;
        }
    }

    private async Task<AppData> FetchAllAppData()
    {
        var answers = await PredictionsData.GetAnswers();
        var circuits = await PredictionsData.GetCircuits();
        var competitors = await PredictionsData.GetCompetitors();
        var drivers = await PredictionsData.GetDrivers();
        var questions = await PredictionsData.GetQuestionResponses();
        var teams = await PredictionsData.GetTeams();

        return new AppData
        (
            answers,
            circuits,
            competitors,
            drivers,
            questions,
            teams
        );
    }

    private Task PersistData()
    {
        if (AppData is not null)
        {
            ApplicationState.PersistAsJson(CascadingConstants.AppStateValueName, AppData);
        }

        return Task.CompletedTask;
    }

    void IDisposable.Dispose()
    {
        _persistingSubscription.Dispose();
    }
}