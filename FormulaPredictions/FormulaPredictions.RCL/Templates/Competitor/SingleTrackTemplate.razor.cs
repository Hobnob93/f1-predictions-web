using FormulaPredictions.Shared.Models;

namespace FormulaPredictions.RCL.Templates.Competitor;

public partial class SingleTrackTemplate : BaseTemplateComponent
{
    private Circuit? _circuit;
    private Circuit Circuit
    {
        get => _circuit ??= GetResponseForCompetitor<Circuit>();
    }
}