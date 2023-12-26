using BlazorComponentUtilities;
using FormulaPredictions.RCL.Services.Implementations;
using FormulaPredictions.RCL.State;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Responses;

public partial class CompetitorResponses : BaseRclComponent
{
    [CascadingParameter]
    private CascadingState AppState { get; set; } = default!;

    [Parameter, EditorRequired]
    public RenderFragment<Competitor> CompetitorTemplate { get; set; } = default!;

    private Competitor[] Competitors => AppState.AppData.Competitors;

    private string Classes => new CssBuilder()
        .AddClass("mt-1")
        .AddClass(Class, when: Class is not null)
        .Build();

    private string? GetCompetitorNote(string competitorId)
    {
        var stars = AppState.Current?.Question.Stars;
        if (stars is null || stars.Length == 0)
            return null;

        return stars.SingleOrDefault(s => s.Target == competitorId)?.Reason;
    }

    private bool CompetitorIsShowingContent(Competitor competitor)
    {
        return AppState.Current?.ShowingCompetitorAnswers.Contains(competitor)
            ?? false;
    }

    private void CompetitorClicked(Competitor competitor)
    {
        if (AppState.Current is null)
            return;

        AppState.Current.ShowingCompetitorAnswers.Add(competitor);
    }
}