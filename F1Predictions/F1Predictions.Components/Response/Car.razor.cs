using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Response
{
    public partial class Car : BaseComponent
    {
        [Parameter, EditorRequired]
        public string TeamId { get; set; } = string.Empty;

        private string _imageSource => "images/2022/cars/aston-martin.png";

        public string Classes => new CssBuilder()
            .AddClass("car")
            .AddClass(Class, when: Class is not null)
            .Build();
    }
}
