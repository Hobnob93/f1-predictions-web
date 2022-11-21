namespace F1Predictions.Core.Models
{
    public class HeadToHead
    {
        public Driver QualiChoice { get; set; }
        public Driver RaceChoice { get; set; }

        public HeadToHead(Driver qualiChoice, Driver raceChoice)
        {
            QualiChoice = qualiChoice;
            RaceChoice = raceChoice;
        }
    }
}
