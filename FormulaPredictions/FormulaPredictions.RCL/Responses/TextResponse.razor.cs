using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FormulaPredictions.RCL.Responses;

public partial class TextResponse : BaseRclComponent
{
    [Parameter, EditorRequired]
    public string Content { get; set; } = string.Empty;

    [Parameter, EditorRequired]
    public string Color { get; set; } = string.Empty;

    [Parameter]
    public bool UseDarkText { get; set; }

    [Parameter]
    public Size Size { get; set; } = Size.Medium;

    public string ColorStyles => new StyleBuilder()
        .AddStyle("font-size", "1.1rem")
        .AddStyle("font-weight", "500")
        .AddStyle("max-width", "none")
        .AddStyle("color", "#1a2030", when: UseDarkText)
        .AddStyle("color", "#f7efdf", when: !UseDarkText)
        .AddStyle("background-color", Color)
        .Build();

    public string Styles => $"{Style} {ColorStyles}";
}