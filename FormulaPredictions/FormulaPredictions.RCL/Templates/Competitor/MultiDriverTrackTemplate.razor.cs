using BlazorComponentUtilities;
using FormulaPredictions.Shared.Models;

namespace FormulaPredictions.RCL.Templates.Competitor;

public partial class MultiDriverTrackTemplate : BaseTemplateComponent
{
    private DriverTrack[]? _driverTracks;
    private DriverTrack[] DriverTracks
    {
        get => _driverTracks ??= ResponsesService.GetDriverTrackResponses(Competitor.Id, AppState.AppData, AppState.Current!);
    }

    private bool CompIsRightAligned => Competitor.IsRightAligned;

    private string DriverTrackContainerClasses => new CssBuilder()
        .AddClass("d-flex")
        .AddClass("flex-row", when: !CompIsRightAligned)
        .AddClass("flex-row-reverse", when: CompIsRightAligned)
        .AddClass("align-end")
        .Build();

    private string DriverClasses => new CssBuilder()
        .AddClass("ms-n4", when: !CompIsRightAligned)
        .AddClass("me-n4", when: CompIsRightAligned)
        .Build();

    private string TrackClasses => new CssBuilder()
        .AddClass("ms-n12", when: !CompIsRightAligned)
        .AddClass("me-n9", when: CompIsRightAligned)
        .Build();
}