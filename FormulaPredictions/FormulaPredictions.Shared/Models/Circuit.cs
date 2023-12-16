using FormulaPredictions.Shared.Models.Base;

namespace FormulaPredictions.Shared.Models;

public class Circuit : BaseItem
{
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
}
