using BlazorComponentUtilities;
using F1Predictions.Core.Interfaces;
using Microsoft.AspNetCore.Components;
using CompetitorData = F1Predictions.Core.Models.Competitor;

namespace F1Predictions.Components.Response
{
    public partial class CompetitorResponses : BaseComponent, IDisposable
    {
        [Inject]
        public ICompetitorsDataService CompetitorsService { get; set; } = default!;

        [Inject]
        public IQuestionsDataService QuestionsService { get; set; } = default!;

        [Parameter, EditorRequired]
        public RenderFragment<CompetitorData> CompetitorTemplate { get; set; } = default!;

        public string Classes => new CssBuilder()
            .AddClass("mt-1")
            .AddClass(Class, when: Class is not null)
            .Build();

        protected override void OnInitialized()
        {
            QuestionsService.StateChanging += OnQuestionAboutToChange;
        }

        private async Task OnQuestionAboutToChange()
        {
            await CompetitorsService.ResetShowingStates();

            await InvokeAsync(StateHasChanged);
        }

        private async Task OnResponseClicked(string competitorId)
        {
            await CompetitorsService.ShowCompetitor(competitorId);
        }

        private string? CompetitorHasNote(string competitorId)
        {
            var stars = QuestionsService.CurrentQuestion?.Stars;
            if (stars is null || stars.Count == 0)
                return null;

            return stars.SingleOrDefault(s => s.Target == competitorId)?.Reason;
        }

        public void Dispose()
        {
            QuestionsService.StateChanging -= OnQuestionAboutToChange;
        }
    }
}
