using FormulaPredictions.Shared.Models;

namespace FormulaPredictions.RCL.Templates.Competitor;

public partial class MultiTrackTemplate : BaseTemplateComponent
{
    private Circuit[]? _circuits;
    private Circuit[] Circuits
    {
        get => _circuits ??= GetResponsesForCompetitor<Circuit>();
    }
}