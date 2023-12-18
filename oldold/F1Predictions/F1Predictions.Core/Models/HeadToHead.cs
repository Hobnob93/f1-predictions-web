namespace F1Predictions.Core.Models
{
    public class HeadToHead
    {
        public List<Driver> DriverOptions { get; set; }

        public Driver QualiChoice { get; set; }
        public Driver RaceChoice { get; set; }

        public HeadToHead(List<Driver> driverOptions, Driver qualiChoice, Driver raceChoice)
        {
            DriverOptions = driverOptions;
            QualiChoice = qualiChoice;
            RaceChoice = raceChoice;
        }
    }
}
