using BlazorComponentUtilities;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Responses;

public partial class TracksResponse : BaseRclComponent
{
    [Parameter, EditorRequired]
    public Circuit[] Circuits { get; set; } = [];

    [Parameter, EditorRequired]
    public string Color { get; set; } = string.Empty;

    [Parameter]
    public bool UseDarkText { get; set; }

    private string Classes => new CssBuilder()
        .AddClass(Class, when: Class is not null)
        .Build();

    private string ContainerStyle => new StyleBuilder()
        .AddStyle("display", "flex")
        .Build();

    private string Styles => $"{Style} {ContainerStyle}";
}