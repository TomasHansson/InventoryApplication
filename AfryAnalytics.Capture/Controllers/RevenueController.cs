using AfryAnalytics.Capture.Enums;
using AfryAnalytics.Capture.Services;
using Microsoft.AspNetCore.Mvc;

namespace AfryAnalytics.Capture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueController : ControllerBase
    {
        private readonly RevenueService _revenueService;
        public RevenueController(RevenueService revenueService)
        {
            _revenueService = revenueService;
        }

        [HttpGet("Revenue/{id}")]
        public async Task<IActionResult> GetSitesRevenue(int id)
        {
            var result = await _revenueService.CalculateRevenue(id);
            return result.Outcome switch
            {
                Outcome.Success => Ok(result.Result),
                Outcome.Error => BadRequest(result.Message ?? "An error occured."),
                Outcome.Exception => StatusCode(500, result.Exception is null ? "Internal server error." : result.Exception.Message),
                _ => StatusCode(500, "Internal server error.")
            };
        }
    }
}
