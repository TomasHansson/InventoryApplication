using AfryAnalytics.Capture.DataAccess;
using AfryAnalytics.Capture.Helpers;
using AfryAnalytics.Capture.Models;
using Microsoft.EntityFrameworkCore;

namespace AfryAnalytics.Capture.Services
{
    public class WindSpeedService
    {
        private readonly CapturePriceDbContext _dbContext;
        public WindSpeedService(CapturePriceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WindSpeed?> GetClosestByLocation(double longitude, double latitude)
        {
            var windSpeeds = await _dbContext.WindSpeeds.ToListAsync();
            if (!windSpeeds.Any())
            {
                return null;
            }

            double minDistance = double.MaxValue;
            WindSpeed? closestWindSpeedLocation = null;
            foreach (var windSpeed in windSpeeds)
            {
                var distance = LocationHelpers.CalculateDistance(windSpeed.Longitude, windSpeed.Latitude, longitude, latitude);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestWindSpeedLocation = windSpeed;
                }
            }
            return closestWindSpeedLocation;
        }
    }
}
