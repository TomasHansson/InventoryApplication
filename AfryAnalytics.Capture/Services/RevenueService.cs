using AfryAnalytics.Capture.Enums;
using AfryAnalytics.Capture.Helpers;
using AfryAnalytics.Capture.Models;

namespace AfryAnalytics.Capture.Services
{
    public class RevenueService
    {
        private readonly MarketService _marketService;
        private readonly SiteService _siteService;
        private readonly WindSpeedService _windSpeedService;

        public RevenueService(MarketService marketService, SiteService siteService, WindSpeedService windSpeedService)
        {
            _marketService = marketService;
            _siteService = siteService;
            _windSpeedService = windSpeedService;
        }

        public async Task<RevenueCalculationResult> CalculateRevenue(int siteId)
        {
            try
            {
                var site = await _siteService.Get(siteId);
                if (site is null)
                {
                    return new RevenueCalculationResult { Outcome = Outcome.Error, Message = "Couldn't find a site with the given SiteId." };
                }

                var market = await _marketService.GetByClosestLocation(site.Longitude, site.Latitude);
                if (market is null)
                {
                    return new RevenueCalculationResult { Outcome = Outcome.Error, Message = $"Couldn't find a Market based on the given sites location ({site.Longitude}:{site.Latitude})." };
                }

                var marketDistance = LocationHelpers.CalculateDistance(site.Longitude, site.Latitude, market.Longitude, market.Latitude);
                if (marketDistance > 15)
                {
                    return new RevenueCalculationResult { Outcome = Outcome.Error, Message = $"No Market found within the area of the given sites location ({site.Longitude}:{site.Latitude})." };
                }

                var windSpeedsForLocation = await _windSpeedService.GetClosestByLocation(site.Longitude, site.Latitude);
                if (windSpeedsForLocation is null)
                {
                    return new RevenueCalculationResult { Outcome = Outcome.Error, Message = $"Couldn't find WindSpeeds based on the given sites location ({site.Longitude}:{site.Latitude})." };
                }

                if (windSpeedsForLocation.AverageWindSpeed <= 0)
                {
                    return new RevenueCalculationResult { Outcome = Outcome.Error, Message = $"The average WindSpeeds for the closests WindSpeeds-Location from the given site ({site.Longitude}:{site.Latitude}) is set to equal or less than 0." };
                }

                var windSpeedsDistance = LocationHelpers.CalculateDistance(site.Longitude, site.Latitude, windSpeedsForLocation.Longitude, windSpeedsForLocation.Latitude);
                if (windSpeedsDistance > 5)
                {
                    return new RevenueCalculationResult { Outcome = Outcome.Error, Message = $"No WindSpeed-Locations were found close enough to the given sites location ({site.Longitude}:{site.Latitude})." };
                }

                var outputRatio = windSpeedsForLocation.AverageWindSpeed / site.WindSpeedAtMaxCapacity;
                var output = site.MaxCapacityMw * site.Availability * outputRatio;
                var revenue = output * market.EurosPerMw;
                return new RevenueCalculationResult { Outcome = Outcome.Success, Result = revenue };
            }
            catch (Exception ex)
            {
                return new RevenueCalculationResult { Outcome = Outcome.Exception, Exception = ex };
            }
        }
    }
}