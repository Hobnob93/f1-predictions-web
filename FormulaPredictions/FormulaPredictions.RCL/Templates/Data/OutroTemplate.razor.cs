using FormulaPredictions.Shared.Models.Charting;
using Microsoft.AspNetCore.Components;
using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.RCL.State;

namespace FormulaPredictions.RCL.Templates.Data;

public partial class OutroTemplate : BaseRclComponent
{
    [Inject]
    private IScoresManager ScoresManager { get; set; } = default!;

    [CascadingParameter]
    protected CascadingState AppState { get; set; } = default!;

    protected List<ChartDataPoint> ChartData { get; set; } = [];

    private Dictionary<Shared.Models.Competitor, double> _competitorScores = [];

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _competitorScores.Clear();
        ScoresManager.CalculateAllScores(AppState.AppData);
        
        foreach (var competitor in AppState.AppData.Competitors)
        {
            _competitorScores.Add(competitor, ScoresManager.GetScoreForCompetitor(competitor.Id));
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        var competitorScores = _competitorScores
            .Select(kvp => (Competitor: kvp.Key, Score: kvp.Value, IsShowing: AppState.Current!.ShowingCompetitorResponses.Contains(kvp.Key)))
            //.Where(d => AppState.Current!.ShowingCompetitorResponses.Contains(d.Competitor))
            .OrderByDescending(d => d.Score);

        ChartData = competitorScores.Select(c => new ChartDataPoint
        {
            Id = c.Competitor.Id,
            Name = c.IsShowing ? c.Competitor.Name : " ??? ",
            Color = c.IsShowing ? c.Competitor.Color : "#FFFFFF",
            Value = (decimal)c.Score,
            ApplicableCompetitors = [c.Competitor]
        }).ToList();
    }

    private void OnSelectedChartDataPoint(ChartDataPoint dataPoint)
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
}