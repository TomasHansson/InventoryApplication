namespace AfryAnalytics.Capture.Models
{
    public class WindSite
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Availability { get; set; } // 0 = 0%, 1 = 100%
        public double MaxCapacityMw { get; set; }
        public double WindSpeedAtMaxCapacity { get; set; }
    }
}
