using FormulaPredictions.Shared.Models.Base;

namespace FormulaPredictions.Shared.Models;

public class Driver : BaseColorItem
{
    public string FirstName { get; set; } = string.Empty;
    public string TeamId { get; set; } = string.Empty;

    public string ImageName => Name.ToLower();
}
