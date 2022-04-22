using AfryAnalytics.Capture.DataAccess;
using AfryAnalytics.Capture.Helpers;
using AfryAnalytics.Capture.Models;
using Microsoft.EntityFrameworkCore;

namespace AfryAnalytics.Capture.Services
{
    public class MarketService
    {
        private readonly CapturePriceDbContext _dbContext;
        public MarketService(CapturePriceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MarketPrices?> GetByClosestLocation(double longitude, double latitude)
        {
            var marketPrices = await _dbContext.MarketPrices.ToListAsync();
            if (!marketPrices.Any())
            {
                return null;
            }

            double minDistance = double.MaxValue;
            MarketPrices? closestMarket = null;
            foreach (var marketPrice in marketPrices)
            {
                var distance = LocationHelpers.CalculateDistance(marketPrice.Longitude, marketPrice.Latitude, longitude, latitude);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestMarket = marketPrice;
                }
            }
            return closestMarket;
        }
    }
}
