using BlazorComponentUtilities;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Responses;

public partial class TeamResponse : BaseRclComponent
{
    [Parameter, EditorRequired]
    public Team Team { get; set; } = default!;

    [Parameter, EditorRequired]
    public int Year { get; set; }

    private string ImageSource => $"images/{Year}/teams/{Team.ImageName}.png";

    private string Classes => new CssBuilder()
        .AddClass("ms-2")
        .AddClass("me-2")
        .AddClass("team")
        .AddClass("hidable-content")
        .AddClass(Class, when: Class is not null)
        .Build();
}