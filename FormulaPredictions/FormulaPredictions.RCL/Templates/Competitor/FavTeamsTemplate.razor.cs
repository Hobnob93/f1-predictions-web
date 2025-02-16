using FormulaPredictions.Shared.Models;

namespace FormulaPredictions.RCL.Templates.Competitor;

public partial class FavTeamsTemplate : CompetitorTemplateComponent
{
    private Team[]? _teams;
    private Team[] Teams
    {
        get => _teams ??= GetResponsesForCompetitor<Team>();
    }
}