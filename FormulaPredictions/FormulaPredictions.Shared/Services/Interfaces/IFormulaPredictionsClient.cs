using FormulaPredictions.Shared.Models;

namespace FormulaPredictions.Shared.Services.Interfaces;

public interface IFormulaPredictionsClient
{
    Task<Answer[]> GetAnswers();
    Task<Competitor[]> GetCompetitors();
    Task<Driver[]> GetDrivers();
    Task<QuestionResponses[]> GetQuestionResponses();
    Task<Team[]> GetTeams();
    Task<Circuit[]> GetCircuits();
}
