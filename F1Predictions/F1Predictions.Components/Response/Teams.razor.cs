using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Response
{
    public partial class Teams : BaseComponent
    {
        [Parameter, EditorRequired]
        public List<string> TeamIds { get; set; } = new();

        private string Classes => new CssBuilder()
            .AddClass(Class, when: Class is not null)
            .Build();

        private string ChildClass => new CssBuilder()
            .AddClass("me-n0", when: TeamIds.Count <= 5)
            .AddClass("me-n5", when: TeamIds.Count < 10 && TeamIds.Count > 5)
            .AddClass("me-n10", when: TeamIds.Count >= 10)
            .Build();

        private string ContainerStyle => new StyleBuilder()
            .AddStyle("display", "flex")
            .Build();

        private string Styles => $"{Style} {ContainerStyle}";
    }
}
