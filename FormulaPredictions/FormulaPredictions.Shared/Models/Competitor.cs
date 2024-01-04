using FormulaPredictions.Shared.Models.Base;

namespace FormulaPredictions.Shared.Models;

public class Competitor : BaseColorItem
{
    public int Index { get; set; }
    public string Nickname { get; set; } = string.Empty;
    public bool UseDarkText { get; set; } = true;

    public bool IsRightAligned => Index % 2 == 1;
}
