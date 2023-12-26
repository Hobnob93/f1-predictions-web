using FormulaPredictions.Shared.Models.Base;

namespace FormulaPredictions.Shared.Models;

public class Competitor : BaseItem
{
    public int Index { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public bool UseDarkText { get; set; } = true;

    public bool IsRightAligned => Index % 2 == 1;
}
