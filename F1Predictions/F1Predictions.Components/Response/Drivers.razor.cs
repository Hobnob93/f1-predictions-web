using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Response
{
    public partial class Drivers : BaseComponent
    {
        [Parameter, EditorRequired]
        public List<string> DriverIds { get; set; } = new();

        private string Classes => new CssBuilder()
            .AddClass(Class, when: Class is not null)
            .Build();

        private string ChildClass => new CssBuilder()
            .AddClass("me-n0", when: DriverIds.Count <= 5)
            .AddClass($"me-n{DriverIds.Count - 6}", when: DriverIds.Count <= 15 && DriverIds.Count > 5)
            .AddClass("me-n10", when: DriverIds.Count > 15)
            .Build();

        private string ContainerStyle => new StyleBuilder()
            .AddStyle("display", "flex")
            .Build();

        private string Styles => $"{Style} {ContainerStyle}";
    }
}
