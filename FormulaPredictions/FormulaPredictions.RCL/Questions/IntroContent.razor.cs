using FormulaPredictions.Shared.Models;
namespace FormulaPredictions.RCL.Questions;

public partial class IntroContent : QuestionContentComponent
{
    private string GetTextContent(Competitor competitor)
    {
        return $"{competitor.Name} A.K.A. '{competitor.Nickname}'";
    }
}