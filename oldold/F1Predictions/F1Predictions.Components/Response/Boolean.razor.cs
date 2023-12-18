using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Response
{
    public partial class Boolean : BaseComponent
    {
        [Parameter, EditorRequired]
        public bool State { get; set; }
    }
}
