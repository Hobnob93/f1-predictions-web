namespace F1Predictions.Core.Models
{
    public class Driver
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string TeamId { get; set; } = string.Empty;

        public string ImageName => LastName.ToLower();
    }
}
