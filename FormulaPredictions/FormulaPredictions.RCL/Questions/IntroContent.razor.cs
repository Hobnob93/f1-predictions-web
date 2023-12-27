using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.RCL.State;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Questions;

public partial class IntroContent : ComponentBase
{
    [Inject]
    private IResponsesService ResponsesService { get; set; } = default!;

    [CascadingParameter]
    private CascadingState AppState { get; set; } = default!;

    private int Year => AppState.AppData.Config.Year;

    private string GetTextContent(Competitor competitor)
    {
        return $"{competitor.Name} A.K.A. '{competitor.Nickname}'";
    }

    private Team GetResponseForCompetitor(Competitor competitor)
    {
        return ResponsesService.GetSingleResponse<Team>(competitor.Id, AppState.AppData, AppState.Current!);
    }
}