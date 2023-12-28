using BlazorComponentUtilities;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Responses;

public partial class DriverResponse : BaseRclComponent
{
    [Parameter, EditorRequired]
    public Driver Driver { get; set; } = default!;

    [Parameter, EditorRequired]
    public int Year { get; set; }

    private string ImageSource => $"images/{Year}/drivers/{Driver.ImageName}.png";

    private string Classes => new CssBuilder()
        .AddClass("ms-2")
        .AddClass("me-2")
        .AddClass("driver")
        .AddClass(Class, when: Class is not null)
        .Build();
}