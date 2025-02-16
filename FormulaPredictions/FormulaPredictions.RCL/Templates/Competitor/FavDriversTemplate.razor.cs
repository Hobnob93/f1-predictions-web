using FormulaPredictions.Shared.Models;

namespace FormulaPredictions.RCL.Templates.Competitor;

public partial class FavDriversTemplate : CompetitorTemplateComponent
{
    private Driver[]? _drivers;
    private Driver[] Drivers
    {
        get => _drivers ??= GetResponsesForCompetitor<Driver>();
    }
}