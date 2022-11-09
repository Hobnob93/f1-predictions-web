using BlazorComponentUtilities;
using F1Predictions.Core.Interfaces;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Response
{
    public partial class Driver : BaseComponent
    {
        private const string ImageSourceFormat = "images/2022/drivers/{0}.png";

        [Inject]
        public IDriversDataService DriversService { get; set; } = default!;

        [Parameter, EditorRequired]
        public string DriverId { get; set; } = string.Empty;

        private F1Predictions.Core.Models.Driver TargetDriver => DriversService.FindItem(DriverId);
        private string ImageSource => string.Format(ImageSourceFormat, TargetDriver.ImageName);

        private string Classes => new CssBuilder()
            .AddClass("ms-2")
            .AddClass("me-2")
            .AddClass("driver")
            .AddClass(Class, when: Class is not null)
            .Build();
    }
}
