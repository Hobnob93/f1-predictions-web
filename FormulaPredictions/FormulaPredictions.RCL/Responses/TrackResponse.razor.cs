using BlazorComponentUtilities;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FormulaPredictions.RCL.Responses;

public partial class TrackResponse : BaseRclComponent
{
    [Parameter, EditorRequired]
    public Circuit Circuit { get; set; } = default!;

    [Parameter, EditorRequired]
    public string Color { get; set; } = string.Empty;

    [Parameter]
    public bool UseDarkText { get; set; }

    [Parameter]
    public Size Size { get; set; } = Size.Medium;

    private string Classes => new CssBuilder()
        .AddClass("ms-2")
        .AddClass("me-2")
        .AddClass(Class, when: Class is not null)
        .Build();
}