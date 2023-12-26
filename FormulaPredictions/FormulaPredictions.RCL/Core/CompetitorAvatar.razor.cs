using ApexCharts;
using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Core;

public partial class CompetitorAvatar : BaseRclComponent
{
    [Parameter, EditorRequired]
    public string Initials { get; set; } = string.Empty;

    [Parameter, EditorRequired]
    public string Color { get; set; } = string.Empty;

    [Parameter]
    public string Name { get; set; } = string.Empty;

    [Parameter]
    public bool Breathe { get; set; } = true;

    [Parameter]
    public string? StarredNote { get; set; }

    private string Classes => new CssBuilder()
        .AddClass("mt-0")
        .AddClass("competitor-avatar")
        .AddClass(Class, when: Class is not null)
        .Build();

    private string ColorStyles => new StyleBuilder()
        .AddStyle("color", Color)
        .AddStyle("background-color", "transparent")
        .Build();

    private string Styles => $"{Style} {ColorStyles}";
}