using BlazorComponentUtilities;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Responses;

public partial class DriversResponse : BaseRclComponent
{
    [Parameter, EditorRequired]
    public Driver[] Drivers { get; set; } = [];

    [Parameter, EditorRequired]
    public int Year { get; set; }

    private string Classes => new CssBuilder()
        .AddClass(Class, when: Class is not null)
        .Build();

    private string ChildClass => new CssBuilder()
        .AddClass("me-n0", when: Drivers.Length <= 5)
        .AddClass($"me-n{Drivers.Length - 6}", when: Drivers.Length <= 15 && Drivers.Length > 5)
        .AddClass("me-n10", when: Drivers.Length > 15)
        .Build();

    private string ContainerStyle => new StyleBuilder()
        .AddStyle("display", "flex")
        .Build();

    private string Styles => $"{Style} {ContainerStyle}";
}