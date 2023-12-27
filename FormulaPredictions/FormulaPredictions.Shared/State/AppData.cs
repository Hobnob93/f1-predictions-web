using FormulaPredictions.Shared.Config;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.Models.Base;

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

    public T[] GetDataArray<T>() where T : BaseItem
    {
        var data = typeof(T) switch
        {
            _ when typeof(T) == typeof(Answer) => Answers.Cast<T>(),
            _ when typeof(T) == typeof(Circuit) => Circuits.Cast<T>(),
            _ when typeof(T) == typeof(Competitor) => Competitors.Cast<T>(),
            _ when typeof(T) == typeof(Driver) => Drivers.Cast<T>(),
            _ when typeof(T) == typeof(QuestionResponses) => Questions.Cast<T>(),
            _ when typeof(T) == typeof(Team) => Teams.Cast<T>(),
            _ => throw new InvalidCastException($"Cannot find data for type '{typeof(T)}'")
        };

        return data.ToArray();
    }
};
