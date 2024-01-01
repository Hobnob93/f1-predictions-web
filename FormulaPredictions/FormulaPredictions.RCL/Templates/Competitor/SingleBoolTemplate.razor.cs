namespace FormulaPredictions.RCL.Templates.Competitor;

public partial class SingleBoolTemplate : CompetitorTemplateComponent
{
    private bool? _state;
    private bool State
    {
        get => _state ??= GetResponseValueForCompetitor<bool>();
    }
}