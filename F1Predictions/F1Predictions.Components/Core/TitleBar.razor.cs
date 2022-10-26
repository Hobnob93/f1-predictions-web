using F1Predictions.Core.Interfaces;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Core
{
    public partial class TitleBar : IDisposable
    {
        [Inject]
        private IQuestionsDataService QuestionsService { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            QuestionsService.StateChanged += OnQuestionChangedAsync;

            await base.OnInitializedAsync();
        }

        private async Task OnQuestionChangedAsync()
        {
            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            QuestionsService.StateChanged -= OnQuestionChangedAsync;
        }
    }
}
