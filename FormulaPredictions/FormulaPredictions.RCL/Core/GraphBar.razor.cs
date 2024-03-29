using BlazorComponentUtilities;
using FormulaPredictions.RCL.State;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FormulaPredictions.RCL.Core;

public partial class GraphBar : BaseRclComponent
{
    [CascadingParameter]
    private CascadingState AppState { get; set; } = default!;

    private bool Open => AppState.Current?.OpenGraphSection ?? false;
    private Type? GraphTemplate => AppState.Current?.GraphResponseTemplate;
    private Color BarColor => Open ? Color.Default : Color.Transparent;
    private string OpenIcon => Open ? Icons.Material.Filled.ArrowCircleDown : Icons.Material.Filled.ArrowCircleUp;

    private void Toggle()
    {
        if (AppState.Current is null)
            return;

        AppState.Current = AppState.Current with
        {
            OpenGraphSection = !Open
        };
    }

    private string BarStyles => new StyleBuilder()
        .AddStyle("transition", "all .6s cubic-bezier(.82,.25,.24,.72) 0s")
        .AddStyle("width", "80%")
        .AddStyle("transform", "translate(12.5%)")
        .AddStyle("border-top-left-radius", "20px", when: Open)
        .AddStyle("border-top-right-radius", "20px", when: Open)
        .AddStyle("border-top-left-radius", "0", when: !Open)
        .AddStyle("border-top-right-radius", "0", when: !Open)
        .AddStyle("border", "0.2em solid var(--mud-palette-primary)", when: Open)
        .AddStyle("border", "0 solid transparent", when: !Open)
        .AddStyle("border-bottom", "0")
        .AddStyle("height", "50%", when: Open)
        .AddStyle("height", "5%", when: !Open)
        .Build();

    private string Styles => $"{Style};{BarStyles}";
}