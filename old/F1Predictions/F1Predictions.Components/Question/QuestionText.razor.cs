using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Question
{
    public partial class QuestionText : BaseComponent
    {
        [Parameter, EditorRequired]
        public string Text { get; set; } = string.Empty;

        [Parameter]
        public string? Scoring { get; set; }

        public string Classes => new CssBuilder()
            .AddClass("mt-n4")
            .AddClass(Class, when: Class is not null)
            .Build();
    }
}
