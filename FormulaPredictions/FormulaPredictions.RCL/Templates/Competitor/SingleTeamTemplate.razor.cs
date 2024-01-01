using FormulaPredictions.Shared.Models;

namespace FormulaPredictions.RCL.Templates.Competitor;

public partial class SingleTeamTemplate : CompetitorTemplateComponent
{
    private Team? _team;
    private Team Team
    {
        get => _team ??= GetResponseForCompetitor<Team>();
    }
}