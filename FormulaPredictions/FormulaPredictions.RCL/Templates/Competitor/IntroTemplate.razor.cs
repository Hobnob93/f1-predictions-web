using FormulaPredictions.Shared.Models;

namespace FormulaPredictions.RCL.Templates.Competitor;

public partial class IntroTemplate : CompetitorTemplateComponent
{
    private Team? _team;
    private Team Team
    {
        get => _team ??= GetResponseForCompetitor<Team>();
    }

    private string TextContent => $"{Competitor.Name} A.K.A. '{Competitor.Nickname}'";
}