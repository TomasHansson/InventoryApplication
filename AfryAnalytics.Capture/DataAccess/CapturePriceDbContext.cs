using AfryAnalytics.Capture.Models;
using Microsoft.EntityFrameworkCore;

namespace AfryAnalytics.Capture.DataAccess
{
    public class CapturePriceDbContext : DbContext
    {
        public CapturePriceDbContext(DbContextOptions<CapturePriceDbContext> options) : base(options) {}

        public DbSet<WindSite> WindSites { get; set; }
        public DbSet<WindSpeed> WindSpeeds { get; set; }
        public DbSet<MarketPrices> MarketPrices { get; set; }
    }
}
