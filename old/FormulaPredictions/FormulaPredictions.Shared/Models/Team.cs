using FormulaPredictions.Shared.Models.Base;

namespace FormulaPredictions.Shared.Models;

public class Team : BaseItem
{
    public string Name { get; set; } = string.Empty;
    public string ImageName { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string[] DriverIds { get; set; } = [];
}
