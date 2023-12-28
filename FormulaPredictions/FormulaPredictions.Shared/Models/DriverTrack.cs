namespace FormulaPredictions.Shared.Models;

public class DriverTrack
{
    public Driver Driver { get; }
    public Circuit Circuit { get; }

    public DriverTrack(Driver driver, Circuit circuit)
    {
        Driver = driver;
        Circuit = circuit;
    }
}
