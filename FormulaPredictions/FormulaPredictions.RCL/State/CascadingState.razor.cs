using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;
using Microsoft.AspNetCore.Components;
using System.Collections.Specialized;

namespace FormulaPredictions.RCL.State;

public partial class CascadingState : ComponentBase
{
    [Parameter, EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;

    private AppData _appData = AppData.CreateDefault();
    public AppData AppData
    {
        get => _appData;
        set
        {
            if (_appData != value)
            {
                _appData = value;
                StateHasChanged();
            }
        }
    }

    private string _appBarTitle = "Loading...";
    public string AppBarText
    {
        get => _appBarTitle;
        set
        {
            if (_appBarTitle != value)
            {
                _appBarTitle = value;
                StateHasChanged();
            }
        }
    }

    private CurrentData? _currentQuestion;
    public CurrentData? Current
    {
        get => _currentQuestion;
        set
        {
            if (_currentQuestion != value)
            {
                if (_currentQuestion is not null)
                    _currentQuestion.ShowingCompetitorAnswers.CollectionChanged -= OnObservableCollectionModified;

                _currentQuestion = value;

                if (_currentQuestion is not null)
                    _currentQuestion.ShowingCompetitorAnswers.CollectionChanged += OnObservableCollectionModified;

                StateHasChanged();
            }
        }
    }

    private void OnObservableCollectionModified(object? sender, NotifyCollectionChangedEventArgs e)
    {
        StateHasChanged();
    }
}