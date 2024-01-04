using FormulaPredictions.Shared.Models.Base;

namespace FormulaPredictions.Shared.Models;

public class Team : BaseColorItem
{
    public string ImageName { get; set; } = string.Empty;
    public string[] DriverIds { get; set; } = [];
}
