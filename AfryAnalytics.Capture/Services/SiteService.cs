using AfryAnalytics.Capture.DataAccess;
using AfryAnalytics.Capture.Models;
using Microsoft.EntityFrameworkCore;

namespace AfryAnalytics.Capture.Services
{
    public class SiteService
    {
        private readonly CapturePriceDbContext _dbContext;
        public SiteService(CapturePriceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WindSite?> Get(int id)
        {
            return await _dbContext.WindSites.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
