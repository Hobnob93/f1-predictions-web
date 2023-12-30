using FormulaPredictions.RCL.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Templates.Data;

public abstract class DataTemplateComponent : BaseTemplateComponent
{
    [Inject]
    protected IAnswersService AnswersService { get; set; } = default!;
}
