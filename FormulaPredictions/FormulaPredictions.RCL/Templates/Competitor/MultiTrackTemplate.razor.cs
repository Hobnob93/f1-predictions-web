using FormulaPredictions.Shared.Models;

namespace FormulaPredictions.RCL.Templates.Competitor;

public partial class MultiTrackTemplate : CompetitorTemplateComponent
{
    private Circuit[]? _circuits;
    private Circuit[] Circuits
    {
        get => _circuits ??= GetResponsesForCompetitor<Circuit>();
    }
}