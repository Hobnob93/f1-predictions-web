using FormulaPredictions.Shared.Config;
using FormulaPredictions.Shared.Models;

namespace FormulaPredictions.Shared.State;

public record AppData
(
    Answer[] Answers,
    Circuit[] Circuits,
    Competitor[] Competitors,
    Driver[] Drivers,
    QuestionResponses[] Questions,
    Team[] Teams,
    GeneralConfig Config,
    bool HasFetched = false
)
{
    public static AppData CreateDefault()
    {
        return new AppData
        (
            Answers: [],
            Circuits: [],
            Competitors: [],
            Drivers: [],
            Questions: [],
            Teams: [],
            Config: new()
        );
    }
};
