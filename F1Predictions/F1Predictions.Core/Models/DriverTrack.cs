namespace F1Predictions.Core.Models
{
    public class DriverTrack
    {
        public Driver Driver { get; }
        public Track Track { get; }

        public DriverTrack(Driver driver, Track track)
        {
            Driver = driver;
            Track = track;
        }
    }
}
