using FormulaPredictions.Shared.State;
using Microsoft.AspNetCore.Components;

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

    private CurrentQuestion? _currentQuestion;
    public CurrentQuestion? CurrentQuestion
    {
        get => _currentQuestion;
        set
        {
            if (_currentQuestion != value)
            {
                _currentQuestion = value;
                StateHasChanged();
            }
        }
    }
}