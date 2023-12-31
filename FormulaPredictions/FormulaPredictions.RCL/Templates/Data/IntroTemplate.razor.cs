using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.Models.Charting;

namespace FormulaPredictions.RCL.Templates.Data;

public partial class IntroTemplate : DataTemplateComponent
{
    protected override void OnInitialized()
    {
        base.OnInitialized();

        var responses = GetResponsesForAllCompetitors<Team>();
        var uniqueTeams = responses
            .Select(r => r.Response)
            .GroupBy(t => t.Id)
            .Select(g => g.First())
            .ToList();

        ResponseData = uniqueTeams.Select(ut => new ChartDataPoint
        {
            Id = ut.Id,
            Name = ut.Name,
            Color = ut.Color,
            Value = responses
                .Count(t => t.Response.Id == ut.Id),
            ApplicableCompetitors = responses
                .Where(r => r.Response.Id == ut.Id)
                .Select(r => r.Competitor)
                .ToArray()
        }).ToList();
    }
}