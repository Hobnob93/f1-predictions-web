using FormulaPredictions.Shared.Models;

namespace FormulaPredictions.RCL.Templates.Competitor;

public partial class SingleDriverTemplate : CompetitorTemplateComponent
{
    private Driver? _driver;
    private Driver Driver
    {
        get => _driver ??= GetResponseForCompetitor<Driver>();
    }
}