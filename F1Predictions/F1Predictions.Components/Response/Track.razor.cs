using BlazorComponentUtilities;
using F1Predictions.Core.Interfaces;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Response
{
    public partial class Track : BaseComponent
    {
        [Inject]
        public ITracksDataService TracksService { get; set; } = default!;

        [Parameter, EditorRequired]
        public string TrackId { get; set; } = string.Empty;

        [Parameter, EditorRequired]
        public string Color { get; set; } = string.Empty;

        [Parameter]
        public bool UseDarkText { get; set; }


        private F1Predictions.Core.Models.Track TargetTrack => TracksService.FindItem(TrackId);

        private string Classes => new CssBuilder()
            .AddClass("ms-2")
            .AddClass("me-2")
            .AddClass(Class, when: Class is not null)
            .Build();
    }
}
