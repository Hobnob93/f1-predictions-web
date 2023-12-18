using FormulaPredictions.Shared.Models.Base;

namespace FormulaPredictions.Shared.Models;

public class Driver : BaseItem
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string TeamId { get; set; } = string.Empty;

    public string ImageName => LastName.ToLower();
}
