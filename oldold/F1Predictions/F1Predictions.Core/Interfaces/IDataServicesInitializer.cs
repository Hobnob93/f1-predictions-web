namespace F1Predictions.Core.Interfaces
{
    public interface IDataServicesInitializer
    {
        Task InitializeCompetitorsAsync();
        Task InitializeTeamsAsync();
        Task InitializeQuestionsAsync();
        Task InitializeDriversAsync();
        Task InitializeTracksAsync();
        Task InitializeAnswersAsync();
    }
}
