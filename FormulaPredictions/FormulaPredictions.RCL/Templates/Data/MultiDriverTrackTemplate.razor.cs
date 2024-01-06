using FormulaPredictions.Shared.Models.Charting;

namespace FormulaPredictions.RCL.Templates.Data;

public partial class MultiDriverTrackTemplate : DataTemplateComponent
{
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        var responses = ResponsesService.GetAllDriverTrackResponses(AppState.AppData, AppState.Current!);
        var unique = responses
            .Select(r => r.Response)
            .GroupBy(r => r.Circuit.Id)
            .Select(g => g.First())
            .OrderByDescending(c => responses.Count(r => r.Response.Circuit.Id == c.Circuit.Id))
            .ToList();

        ChartData = unique.Select(u => new ChartDataPoint
        {
            Id = u.Circuit.Id,
            Name = u.Circuit.Name,
            Color = u.Circuit.Color,
            Value = responses
                .Count(t => t.Response.Circuit.Id == u.Circuit.Id),
            ApplicableCompetitors = responses
                .Where(r => r.Response.Circuit.Id == u.Circuit.Id)
                .Select(r => r.Competitor)
                .ToArray()
        }).ToList();
    }
}