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

    [Inject]
    private IScoresManager ScoresManager { get; set; } = default!;

    [CascadingParameter]
    private CascadingState AppState { get; set; } = default!;

    [Parameter]
    public RenderFragment<Competitor>? CompetitorTemplate { get; set; }

    private IScoringSystem? ScoringSystem { get; set; }

    private Competitor[] _competitors = [];
    private bool _isFinalQuestion = false;

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (!_isFinalQuestion)
        {
            ScoringSystem = ScoringSystemFactory.CreateSystemFromScoreType(AppState.Current?.Question.Scoring.Type ?? ScoringType.None);
            _isFinalQuestion = AppState.Current?.Question.Id == AppState.AppData.Questions.Last().Id;

            if (firstRender)
            {
                _competitors = AppState.AppData.Competitors;
                StateHasChanged();
            }

            if (_isFinalQuestion)
            {
                _competitors = _competitors
                    .OrderByDescending(c => ScoresManager.GetScoreForCompetitor(c.Id))
                    .ToArray();

                StateHasChanged();
            }
        }
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

    private string GetCompetitorInitials(Competitor competitor)
    {
        if (_isFinalQuestion)
            return (Array.IndexOf(_competitors, competitor) + 1).ToString();

        return competitor.Id;
    }

    private string GetCompetitorColor(Competitor competitor)
    {
        if (_isFinalQuestion)
        {
            var isShowingContent = CompetitorIsShowingContent(competitor);
            return isShowingContent ? competitor.Color : "#FFFFFF";
        }

        return competitor.Color;
    }

    private bool IsCompetitorRightAligned(Competitor competitor)
    {
        if (_isFinalQuestion)
        {
            var index = Array.IndexOf(_competitors, competitor);
            return index % 2 == 1;
        }

        return competitor.IsRightAligned;
    }
}