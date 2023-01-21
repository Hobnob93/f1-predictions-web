using BlazorComponentUtilities;
using F1Predictions.Core.Interfaces;
using Microsoft.AspNetCore.Components;
using CompetitorData = F1Predictions.Core.Models.Competitor;

namespace F1Predictions.Components.Response
{
    public partial class CompetitorResults : BaseComponent, IDisposable
    {
        [Inject]
        public ICompetitorsDataService CompetitorsService { get; set; } = default!;

        [Inject]
        public IQuestionsDataService QuestionsService { get; set; } = default!;

        [Inject]
        public IScoreManager ScoreManager { get; set; } = default!;

        [Parameter, EditorRequired]
        public RenderFragment<CompetitorData> CompetitorTemplate { get; set; } = default!;

        private List<CompetitorData> Competitors { get; set; } = new List<CompetitorData>();

        protected override void OnInitialized()
        {
            QuestionsService.StateChanging += OnQuestionAboutToChange;

            Competitors = ScoreManager.GetOrderedCompetitors()
                .Select(id => CompetitorsService.FindItem(id))
                .ToList();

            var index = 0;
            foreach (var competitor in Competitors)
            {
                competitor.Index = index;
                index++;
            }
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

        public void Dispose()
        {
            QuestionsService.StateChanging -= OnQuestionAboutToChange;
        }

        public string Classes => new CssBuilder()
            .AddClass("mt-1")
            .AddClass(Class, when: Class is not null)
            .Build();
    }
}
