using FormulaPredictions.Shared.Models.Base;

namespace FormulaPredictions.Shared.Models.Charting;

public class RawCompetitorResponse<T> where T : BaseItem
{
    public required Competitor Competitor { get; set; }
    public required T Response { get; set; }
}
