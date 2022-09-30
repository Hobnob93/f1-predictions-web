using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Competitors
{
    public partial class Competitor
    {
        [Parameter, EditorRequired]
        public int Index { get; set; }

        [Parameter]
        public string Initials { get; set; } = string.Empty;

        [Parameter]
        public string TeamId { get; set; } = string.Empty;

        private bool IsLeft => Index % 2 == 0;
    }
}
