using FormulaPredictions.Shared.Models.Base;

namespace FormulaPredictions.Shared.Models.Charting;

public class ListDataPoint : BaseItem
{
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = "#CFCFCF";
}
