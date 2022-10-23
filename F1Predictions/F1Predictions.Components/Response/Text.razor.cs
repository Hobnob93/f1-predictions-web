using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Response
{
    public partial class Text : BaseComponent
    {
        [Parameter, EditorRequired]
        public string Content { get; set; } = string.Empty;

        [Parameter, EditorRequired]
        public string Color { get; set; } = string.Empty;

        [Parameter]
        public bool UseDarkText { get; set; }

        public string ColorStyles => new StyleBuilder()
            .AddStyle("font-weight", "600")
            .AddStyle("color", "#2a3040", when: UseDarkText)
            .AddStyle("color", "#e6dfcf", when: !UseDarkText)
            .AddStyle("background-color", Color)
            .Build();

        public string Styles => $"{Style} {ColorStyles}";
    }
}
