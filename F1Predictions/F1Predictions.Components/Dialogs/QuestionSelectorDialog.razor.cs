using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace F1Predictions.Components.Dialogs
{
    public partial class QuestionSelectorDialog
    {
        [CascadingParameter]
        private MudDialogInstance Dialog { get; set; } = default!;

        private void Cancel()
        {
            Dialog.Cancel();
        }
    }
}
