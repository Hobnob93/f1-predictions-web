using FormulaPredictions.RCL.State;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Charting;

public partial class ShowAnswerContainer : BaseRclComponent
{
    [Parameter, EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;

    [CascadingParameter]
    public CascadingState State { get; set; } = default!;

    public bool ShowingActual => State.Current?.ShowActual ?? false;
    public bool OpenGraphSection => State.Current?.OpenGraphSection ?? false;

    protected void ShowActualClicked(MouseEventArgs e)
    {
        if (State.Current is null)
            return;

        State.Current = State.Current with
        {
            ShowActual = true
        };
    }
}