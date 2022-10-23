using BlazorComponentUtilities;
using F1Predictions.Core.Interfaces;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Response
{
    public partial class Car : BaseComponent
    {
        private const string ImageSourceFormat = "images/2022/cars/{0}.png";

        [Inject]
        public ITeamsDataService TeamsService { get; set; } = default!;

        [Parameter, EditorRequired]
        public string TeamId { get; set; } = string.Empty;

        private string ImageSource => string.Format(ImageSourceFormat, TeamsService.FindItem(TeamId).ImageName);

        public string Classes => new CssBuilder()
            .AddClass("car")
            .AddClass(Class, when: Class is not null)
            .Build();
    }
}
