namespace FormulaPredictions.RCL.Templates.Competitor;

public partial class IntroTemplate : BaseTemplateComponent
{
    private string GetTextContent()
    {
        return $"{Competitor.Name} A.K.A. '{Competitor.Nickname}'";
    }
}