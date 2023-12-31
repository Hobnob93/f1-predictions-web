using FormulaPredictions.Shared.Models.Charting;
using FormulaPredictions.Shared.Models;

namespace FormulaPredictions.RCL.Templates.Data;

public partial class SingleTeamTemplate : DataTemplateComponent
{
    protected override void OnInitialized()
    {
        base.OnInitialized();

        var responses = GetResponsesForAllCompetitors<Team>();
        var unique = responses
            .Select(r => r.Response)
            .GroupBy(t => t.Id)
            .Select(g => g.First())
            .ToList();

        ResponseData = unique.Select(u => new ChartDataPoint
        {
            Id = u.Id,
            Name = u.Name,
            Color = u.Color,
            Value = responses
                .Count(t => t.Response.Id == u.Id),
            ApplicableCompetitors = responses
                .Where(r => r.Response.Id == u.Id)
                .Select(r => r.Competitor)
                .ToArray()
        }).ToList();
    }
}