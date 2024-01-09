using BlazorComponentUtilities;
using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.RCL.State;
using FormulaPredictions.Shared.Enums;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Responses;

public partial class CompetitorResponses : BaseRclComponent
{
    [Inject]
    private IScoringSystemFactory ScoringSystemFactory { get; set; } = default!;

    [CascadingParameter]
    private CascadingState AppState { get; set; } = default!;

    [Parameter]
    public RenderFragment<Competitor>? CompetitorTemplate { get; set; }

    private IScoringSystem? ScoringSystem { get; set; }

    private Competitor[] Competitors => AppState.AppData.Competitors;

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        ScoringSystem = ScoringSystemFactory.CreateSystemFromScoreType(AppState.Current?.Question.Scoring.Type ?? ScoringType.None);
    }

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
        return AppState.Current?.ShowingCompetitorResponses.Contains(competitor)
            ?? false;
    }

    private void CompetitorClicked(Competitor competitor)
    {
        if (AppState.Current is null)
            return;

        AppState.Current.ShowingCompetitorResponses.Add(competitor);
    }

    private double GetCompetitorScore(Competitor competitor)
    {
        if (ScoringSystem is null)
            return 0d;

        if (AppState.Current?.ShowScores != true)
            return 0d;

        return ScoringSystem.CalculateScoreForCompetitor(competitor, AppState.AppData, AppState.Current!);
    }
}