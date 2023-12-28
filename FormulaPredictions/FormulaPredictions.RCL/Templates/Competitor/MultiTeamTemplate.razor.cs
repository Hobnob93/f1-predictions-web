using FormulaPredictions.Shared.Models;

namespace FormulaPredictions.RCL.Templates.Competitor;

public partial class MultiTeamTemplate : BaseTemplateComponent
{
    private Team[]? _teams;
    private Team[] Teams
    {
        get => _teams ??= GetResponsesForCompetitor<Team>();
    }
}