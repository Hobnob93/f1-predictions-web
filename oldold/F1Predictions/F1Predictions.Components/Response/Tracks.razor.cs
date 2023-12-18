using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Response
{
    public partial class Tracks
    {
        [Parameter, EditorRequired]
        public List<string> TrackIds { get; set; } = new();

        [Parameter, EditorRequired]
        public string Color { get; set; } = string.Empty;

        [Parameter]
        public bool UseDarkText { get; set; }

        private string Classes => new CssBuilder()
            .AddClass(Class, when: Class is not null)
            .Build();

        private string ContainerStyle => new StyleBuilder()
            .AddStyle("display", "flex")
            .Build();

        private string Styles => $"{Style} {ContainerStyle}";
    }
}
