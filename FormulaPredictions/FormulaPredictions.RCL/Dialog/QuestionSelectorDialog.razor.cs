using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.RCL.State;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FormulaPredictions.RCL.Dialog;

public partial class QuestionSelectorDialog
{
    [Inject]
    private IQuestionsService QuestionsService { get; set; } = default!;

    [CascadingParameter]
    private MudDialogInstance Dialog { get; set; } = default!;

    [CascadingParameter]
    private CascadingState AppState { get; set; } = default!;

    private IGrouping<char, QuestionResponses>[] QuestionGroups => QuestionsService.GetGroupings(AppState.AppData);

    private void Cancel()
    {
        Dialog.Cancel();
    }

    private void OnQuestionSelected(QuestionResponses question)
    {
        Dialog.Cancel();
        
        AppState.Current = new CurrentData
        (
            Question: question,
            ShowingCompetitorResponses: [],
            OpenGraphSection: false,
            ShowActual: false,
            ShowScores: false
        );
    }
}