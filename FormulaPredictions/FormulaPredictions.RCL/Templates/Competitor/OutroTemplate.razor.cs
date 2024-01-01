namespace FormulaPredictions.RCL.Templates.Competitor;

public partial class OutroTemplate : CompetitorTemplateComponent
{
    private string TextContent => $"{Competitor.Name} A.K.A. '{Competitor.Nickname}'";
}