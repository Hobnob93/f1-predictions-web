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

        [Parameter, EditorRequired]
        public string Color { get; set; }

        private bool IsRightAligned => Index % 2 == 1;

        private string OuterClasses => new CssBuilder()
            .AddClass("d-flex")
            .AddClass("z-10")
            .AddClass("mb-2")
            .AddClass("gap-5")
            .AddClass("nameplate")
            .AddClass("mr-2", when: !IsRightAligned)
            .AddClass("ml-2", when: IsRightAligned)
            .AddClass("ml-n11", when: !IsRightAligned)
            .AddClass("mr-n11", when: IsRightAligned)
            .AddClass("flex-row", when: !IsRightAligned)
            .AddClass("flex-row-reverse", when: IsRightAligned)
            .AddClass(Class, when: Class is not null)
            .Build();

        private string InnerClasses => new CssBuilder()
            .AddClass("d-flex")
            .AddClass("align-center")
            .AddClass("competitor")
            .AddClass("nameplate")
            .AddClass("breathe")
            .AddClass("pe-2", when: !IsRightAligned)
            .AddClass("ps-2", when: IsRightAligned)
            .AddClass("flex-row", when: !IsRightAligned)
            .AddClass("flex-row-reverse", when: IsRightAligned)
            .Build();

        private string InnerStyle => new StyleBuilder()
            .AddStyle("color", Color)
            .AddStyle("border-color", Color)
            .Build();
    }
}