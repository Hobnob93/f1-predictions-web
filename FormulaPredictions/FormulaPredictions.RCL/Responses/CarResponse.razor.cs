using BlazorComponentUtilities;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Responses;

public partial class CarResponse : BaseRclComponent
{
    [Parameter, EditorRequired]
    public Team Team { get; set; } = default!;

    [Parameter, EditorRequired]
    public int Year { get; set; }

    private string ImageSource => $"images/{Year}/cars/{Team.ImageName}.png";

    private string Classes => new CssBuilder()
            .AddClass("car")
            .AddClass(Class, when: Class is not null)
            .Build();
}