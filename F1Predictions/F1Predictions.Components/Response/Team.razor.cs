using BlazorComponentUtilities;
using F1Predictions.Core.Interfaces;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Response
{
    public partial class Team : BaseComponent
    {
        private const string ImageSourceFormat = "images/2022/teams/{0}.png";

        [Inject]
        public ITeamsDataService TeamsService { get; set; } = default!;

        [Parameter, EditorRequired]
        public string TeamId { get; set; } = string.Empty;

        private F1Predictions.Core.Models.Team TargetTeam => TeamsService.FindItem(TeamId);
        private string ImageSource => string.Format(ImageSourceFormat, TargetTeam.ImageName);
        private string TeamColor => TargetTeam.Color;

        private string Classes => new CssBuilder()
            .AddClass("team")
            .AddClass("breathe")
            .AddClass(Class, when: Class is not null)
            .Build();

        private string ColorStyles => new StyleBuilder()
            .AddStyle("border-color", TeamColor)
            .AddStyle("color", TeamColor)
            .Build();

        private string Styles => $"{ColorStyles} {Style}";
    }
}
