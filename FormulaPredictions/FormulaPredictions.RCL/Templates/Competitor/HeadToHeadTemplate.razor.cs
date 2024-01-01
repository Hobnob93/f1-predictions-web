using BlazorComponentUtilities;
using FormulaPredictions.Shared.Models;

namespace FormulaPredictions.RCL.Templates.Competitor;

public partial class HeadToHeadTemplate : CompetitorTemplateComponent
{
    private Driver[]? _drivers;
    private Driver[] Drivers
    {
        get => _drivers ??= GetResponsesForCompetitor<Driver>();
    }

    private Driver QualiChoice => Drivers.First();
    private Driver RaceChoice => Drivers.Last();
    private bool CompIsRightAligned => Competitor.IsRightAligned;

    private string ContainerClasses => new CssBuilder()
        .AddClass("d-flex")
        .AddClass("flex-row", when: !CompIsRightAligned)
        .AddClass("flex-row-reverse", when: CompIsRightAligned)
        .AddClass("align-end")
        .Build();

    private string DriverClasses => new CssBuilder()
        .AddClass("ms-n4", when: !CompIsRightAligned)
        .AddClass("me-n4", when: CompIsRightAligned)
        .Build();

    private string TextClasses => new CssBuilder()
        .AddClass("ms-n12", when: !CompIsRightAligned)
        .AddClass("me-n9", when: CompIsRightAligned)
        .Build();
}