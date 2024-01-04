using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.Models.Charting;

namespace FormulaPredictions.RCL.Templates.Data;

public partial class IntroTemplate : DataTemplateComponent
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

        ChartData = unique.Select(u => new ChartDataPoint
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