using FormulaPredictions.RCL.State;
using FormulaPredictions.Shared.Services.Interfaces;
using FormulaPredictions.Shared.State;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.Client.Pages;

public partial class FetchData : ComponentBase
{
    [Inject]
    private IPredictionsData Client { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    [CascadingParameter]
    private CascadingState AppState { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        AppState.Title = "Loading...";

        await FetchAllAppData();
        NavigationManager.NavigateTo("/");
    }

    private async Task FetchAllAppData()
    {
        var answers = await Client.GetAnswers();
        var circuits = await Client.GetCircuits();
        var competitors = await Client.GetCompetitors();
        var drivers = await Client.GetDrivers();
        var questions = await Client.GetQuestionResponses();
        var teams = await Client.GetTeams();

        AppState.AppData = new AppData
        (
            answers,
            circuits,
            competitors,
            drivers,
            questions,
            teams,
            HasFetched: true
        );
    }
}