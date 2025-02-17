using FormulaPredictions.Shared.Models.Charting;
using FormulaPredictions.Shared.Models;

namespace FormulaPredictions.RCL.Templates.Data;

public partial class SingleBoolTemplate : DataTemplateComponent
{
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        var responses = GetValueResponsesForAll<bool>();
        var unique = responses
            .Select(r => r.Response)
            .Distinct()
            .OrderByDescending(b => responses.Count(r => r.Response == b))
            .ToList();

        ChartData = unique.Select(b => new ChartDataPoint
        {
            Id = b ? "Yes" : "No",
            Name = b ? "Yes" : "No",
            Color = b ? "#0BBA83" : "#F64E62",
            Value = responses
                .Count(r => r.Response == b),
            ApplicableCompetitors = responses
                .Where(r => r.Response == b)
                .Select(r => r.Competitor)
                .ToArray()
        }).ToList();
    }
}