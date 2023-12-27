using BlazorComponentUtilities;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Responses;

public partial class TeamsResponse : BaseRclComponent
{
    [Parameter, EditorRequired]
    public Team[] Teams { get; set; } = [];

    [Parameter, EditorRequired]
    public int Year { get; set; }

    private string Classes => new CssBuilder()
        .AddClass(Class, when: Class is not null)
        .Build();

    private string ChildClass => new CssBuilder()
        .AddClass("me-n0", when: Teams.Length <= 5)
        .AddClass($"me-n{Teams.Length - 6}", when: Teams.Length <= 15 && Teams.Length > 5)
        .AddClass("me-n10", when: Teams.Length > 15)
        .Build();

    private string ContainerStyle => new StyleBuilder()
        .AddStyle("display", "flex")
        .Build();

    private string Styles => $"{Style} {ContainerStyle}";
}