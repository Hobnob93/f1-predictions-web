using FormulaPredictions.RCL.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL.Templates.Competitor;

public partial class OutroTemplate : CompetitorTemplateComponent
{
    [Inject]
    private IScoresManager ScoresManager { get; set; } = default!;

    private string TextContent => $"{Competitor.Name} A.K.A. '{Competitor.Nickname}'";
    private string ScoreContent => ScoresManager.GetScoreForCompetitor(Competitor.Id).ToString();
}