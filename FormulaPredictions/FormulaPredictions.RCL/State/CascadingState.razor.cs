using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Enums;
using FormulaPredictions.Shared.State;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FormulaPredictions.RCL.State;

public partial class CascadingState : ComponentBase
{
    [Inject]
    private IScoresManager ScoresManager { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

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

    private CurrentData? _currentData;
    public CurrentData? Current
    {
        get => _currentData;
        set
        {
            if (_currentData != value)
            {
                CurrentDataChanging(_currentData, value);
                _currentData = value;
                StateHasChanged();
            }
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Snackbar.Configuration.SnackbarVariant = Variant.Outlined;
        Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
        Snackbar.Configuration.ShowCloseIcon = false;
        Snackbar.Configuration.VisibleStateDuration = 5000;
        Snackbar.Configuration.HideTransitionDuration = 200;
        Snackbar.Configuration.ShowTransitionDuration = 200;
    }

    public void CurrentDataChanging(CurrentData? oldData, CurrentData? newData)
    {
        if (oldData is null || newData is null)
            return;

        if (oldData.Question.Id == newData.Question.Id)
            return;

        if (oldData.Question.Scoring.Type == ScoringType.None)
            return;

        var (topScore, topScorers) = ScoresManager.GetHighestScorers(AppData, oldData);
        if (topScore == 0)
        {
            Snackbar.Add("No points gained in that last question!", Severity.Normal);
        }
        else if (topScorers.Length == AppData.Competitors.Length)
        {
            Snackbar.Add($"EVERYONE scored {topScore} that round...", Severity.Info);
        }
        else if (topScorers.Length == 1)
        {
            Snackbar.Add($"{topScorers.First().Id} was top with {topScore}", Severity.Success);
        }
        else
        {
            var ids = string.Join(", ", topScorers.Select(x => x.Id));
            Snackbar.Add($"{ids} were top with {topScore}", Severity.Success);
        }
    }
}