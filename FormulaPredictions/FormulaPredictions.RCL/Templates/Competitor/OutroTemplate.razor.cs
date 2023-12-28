namespace FormulaPredictions.RCL.Templates.Competitor;

public partial class OutroTemplate : BaseTemplateComponent
{
    private string TextContent => $"{Competitor.Name} A.K.A. '{Competitor.Nickname}'";
}