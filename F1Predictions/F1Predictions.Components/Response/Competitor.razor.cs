using BlazorComponentUtilities;
using F1Predictions.Core.Interfaces;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Response
{
    public partial class Competitor : BaseComponent
    {
        [Parameter, EditorRequired]
        public string Initials { get; set; } = string.Empty;

        [Parameter, EditorRequired]
        public string Color { get; set; } = string.Empty;

        [Parameter]
        public string Name { get; set; } = string.Empty;

        [Parameter]
        public bool Breathe { get; set; } = true;

        private string Classes => new CssBuilder()
            .AddClass("competitor")
            .AddClass("nameplate")
            .AddClass(Class, when: Class is not null)
            .AddClass("breathe", when: Breathe)
            .Build();

        private string ColorStyles => new StyleBuilder()
            .AddStyle("color", Color)
            .Build();

        private string Styles => $"{Style} {ColorStyles}";
    }
}
