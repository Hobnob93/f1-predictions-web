using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Core
{
    public partial class SectionBar
    {
        [Parameter, EditorRequired]
        public string Title { get; set; } = string.Empty;
    }
}
