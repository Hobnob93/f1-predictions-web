using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Competitors
{
    public partial class Competitor
    {
        [Parameter]
        public string Initials { get; set; } = string.Empty;

        [Parameter]
        public string TeamId { get; set; } = string.Empty;
    }
}
