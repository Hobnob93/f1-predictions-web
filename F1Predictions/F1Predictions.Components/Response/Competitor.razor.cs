using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Response
{
    public partial class Competitor : BaseComponent
    {
        [Parameter, EditorRequired]
        public string Initials { get; set; } = string.Empty;

        [Parameter]
        public string Name { get; set; } = string.Empty;

        [Parameter]
        public string? TeamId { get; set; }

        [Parameter]
        public bool Breathe { get; set; }

        private string Classes => new CssBuilder()
            .AddClass("competitor")
            .AddClass("nameplate")
            .AddClass(TeamId, when: TeamId is not null)
            .AddClass($"{TeamId}-border", when: TeamId is not null)
            .AddClass("null-team", when: TeamId is null)
            .AddClass("null-team-border", when: TeamId is null)
            .AddClass(Class, when: Class is not null)
            .AddClass("breathe", when: Breathe)
            .Build();
    }
}
