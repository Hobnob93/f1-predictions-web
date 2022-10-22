using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Response
{
    public partial class ResponseContainer : BaseComponent
    {
        [Parameter, EditorRequired]
        public RenderFragment ChildContent { get; set; } = default!;

        [Parameter, EditorRequired]
        public int Index { get; set; }

        private bool IsRightAligned => Index % 2 == 1;

        private string Classes => new CssBuilder()
            .AddClass("d-flex")
            .AddClass("z-10")
            .AddClass("flex-row", when: !IsRightAligned)
            .AddClass("flex-row-reverse", when: IsRightAligned)
            .AddClass("ml-n11", when: !IsRightAligned)
            .AddClass("mr-n11", when: IsRightAligned)
            .AddClass(Class, when: Class is not null)
            .Build();
    }
}
