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

        AppState.AppBarText = "Loading...";

        await FetchAllAppData();
        AppState.Current = new CurrentData
        (
            Question: AppState.AppData.Questions.First(),
            ShowingCompetitorAnswers: [AppState.AppData.Competitors.First()]
        );

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
        var config = await Client.GetConfig();

        AppState.AppData = new AppData
        (
            answers,
            circuits,
            competitors,
            drivers,
            questions,
            teams,
            config,
            HasFetched: true
        );
    }
}