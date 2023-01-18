using F1Predictions.Components.Dialogs;
using F1Predictions.Core.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace F1Predictions.Components.Core
{
    public partial class TitleBar : IDisposable
    {
        [Inject]
        private IQuestionsDataService QuestionsService { get; set; } = default!;

        [Inject]
        private IDialogService DialogService { get; set; } = default!;

        [Inject]
        private IScoreManager ScoreManager { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            QuestionsService.StateChanged += OnQuestionChangedAsync;

            await base.OnInitializedAsync();
        }

        private async Task OnQuestionChangedAsync()
        {
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnForwardClicked(MouseEventArgs e)
        {
            if (QuestionsService.CanGoForward())
            {
                await QuestionsService.Next();
            }
        }

        private async Task OnBackClicked(MouseEventArgs e)
        {
            if (QuestionsService.CanGoBack())
            {
                await QuestionsService.Previous();
            }
        }

        private void OnQuestionIdClicked(MouseEventArgs e)
        {
            DialogService.Show<QuestionSelectorDialog>("Select Target Question");
        }

        private void OnUpdateScoresClicked(MouseEventArgs e)
        {
            ScoreManager.UpdateScoresForQuestion();
        }

        public void Dispose()
        {
            QuestionsService.StateChanged -= OnQuestionChangedAsync;
        }

        private string GetTitle()
        {
            var questionResponse = QuestionsService.CurrentQuestion;
            var firstCharId = questionResponse?.Id.First();

            return firstCharId switch
            {
                '0' => "Competitors",
                '1' => "Championship Predictions",
                '2' => "Team Predictions",
                '3' => "Driver Predictions",
                '4' => "Race Weekend Predictions",
                '5' => "Event Predictions",
                '6' => "Head to Heads",
                '7' => "Championship Order",
                '8' => "Best Predictor...",
                _ => string.Empty
            };
        }
    }
}
