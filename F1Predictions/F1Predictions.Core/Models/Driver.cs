namespace F1Predictions.Core.Models
{
    public class Driver : DataItem
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string TeamId { get; set; } = string.Empty;

        public string ImageName => LastName.ToLower();
    }
}
