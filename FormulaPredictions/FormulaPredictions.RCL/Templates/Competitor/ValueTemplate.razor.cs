namespace FormulaPredictions.RCL.Templates.Competitor;

public partial class ValueTemplate : BaseTemplateComponent
{
    private string? _value;
    private string Value
    {
        get => _value ??= GetResponseValueForCompetitor<int>().ToString();
    }
}