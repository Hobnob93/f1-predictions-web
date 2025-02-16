using FormulaPredictions.Shared.Models.Charting;

namespace FormulaPredictions.RCL.Templates.Data;

public partial class ValueTemplate : DataTemplateComponent
{
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        var responses = GetValueResponsesForAll<int>();
        var unique = responses
            .OrderByDescending(r => r.Response)
            .DistinctBy(r => r.Response);

        var colors = AppState.AppData.Teams
            .Select(t => t.Color)
            .ToArray();

        ChartData = unique.Select((u, i) => new ChartDataPoint
        {
            Id = u.Response.ToString(),
            Name = u.Response.ToString(),
            Color = colors[i],
            Value = u.Response,
            ApplicableCompetitors = responses
                .Where(r => r.Response == u.Response)
                .Select(r => r.Competitor)
                .ToArray()
        }).ToList();
    }
}