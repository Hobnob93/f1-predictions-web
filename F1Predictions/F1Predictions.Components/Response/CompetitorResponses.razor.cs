using BlazorComponentUtilities;
using F1Predictions.Core.Interfaces;
using Microsoft.AspNetCore.Components;
using CompetitorData = F1Predictions.Core.Models.Competitor;

namespace F1Predictions.Components.Response
{
    public partial class CompetitorResponses : BaseComponent
    {
        [Inject]
        public ICompetitorsDataService CompetitorsService { get; set; } = default!;

        [Parameter, EditorRequired]
        public RenderFragment<CompetitorData> CompetitorTemplate { get; set; } = default!;

        public string Classes => new CssBuilder()
            .AddClass("mt-1")
            .AddClass(Class, when: Class is not null)
            .Build();
    }
}
