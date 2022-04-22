using AfryAnalytics.Capture.Enums;

namespace AfryAnalytics.Capture.Models
{
    public class RevenueCalculationResult
    {
        public Outcome Outcome { get; set; }
        public string? Message { get; set; }
        public Exception? Exception { get; set; }
        public double Result { get; set; }
    }
}
