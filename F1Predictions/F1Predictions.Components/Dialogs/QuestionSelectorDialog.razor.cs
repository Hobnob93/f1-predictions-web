using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace F1Predictions.Components.Dialogs
{
    public partial class QuestionSelectorDialog
    {
        [CascadingParameter]
        private MudDialogInstance Dialog { get; set; } = default!;

        [Inject]
        private IQuestionsDataService QuestionsService { get; set; } = default!;

        private List<IGrouping<char, QuestionResponse>> QuestionGroups => QuestionsService.QuestionGroups;

        private void Cancel()
        {
            Dialog.Cancel();
        }

        private async Task OnQuestionSelected(QuestionResponse question)
        {
            Dialog.Cancel();

            await QuestionsService.GoTo(question);
        }
    }
}
