using FormulaPredictions.RCL.Dialog;
using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.RCL.State;
using FormulaPredictions.Shared.State;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace FormulaPredictions.RCL.Core;

public partial class TitleBar : ComponentBase
{
    [Inject]
    private IQuestionsService QuestionsService { get; set; } = default!;

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    [CascadingParameter]
    private CascadingState AppState { get; set; } = default!;

    private string GetQuestionGroupName()
    {
        return QuestionsService.GetCurrentGroup(AppState.Current);
    }

    private string GetQuestionId()
    {
        return QuestionsService.GetCurrentId(AppState.Current);
    }

    private bool CanGoBack()
    {
        return QuestionsService.Previous(AppState.Current, AppState.AppData) is not null;
    }

    private bool CanGoForward()
    {
        return QuestionsService.Next(AppState.Current, AppState.AppData) is not null;
    }

    private void OnBackClicked(MouseEventArgs e)
    {
        var previousQuestion = QuestionsService.Previous(AppState.Current, AppState.AppData);
        if (AppState.Current is null || previousQuestion is null)
            return;

        AppState.Current = new CurrentData
        (
            Question: previousQuestion,
            ShowingCompetitorAnswers: [],
            OpenGraphSection: false
        );
    }

    private void OnForwardClicked(MouseEventArgs e)
    {
        var nextQuestion = QuestionsService.Next(AppState.Current, AppState.AppData);
        if (AppState.Current is null || nextQuestion is null)
            return;

        AppState.Current = new CurrentData
        (
            Question: nextQuestion,
            ShowingCompetitorAnswers: [],
            OpenGraphSection: false
        );
    }

    private void OnQuestionIdClicked(MouseEventArgs e)
    {
        DialogService.Show<QuestionSelectorDialog>("Select Target Question");
    }

    private void OnUpdateScoresClicked(MouseEventArgs e)
    {
        
    }
}
