using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_api.Services.SiteInfoService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ecommerce_api.DTO.Stats;
namespace ecommerce_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiteInfoController : ControllerBase
    {
        private readonly ISiteInfoService _siteInfoService;
        private readonly IDashboardService _dashboardService;
        private readonly ILogger<SiteInfoController> _logger;
        public SiteInfoController(ISiteInfoService siteInfoService, ILogger<SiteInfoController> logger, IDashboardService dashboardService)
        {
            _siteInfoService = siteInfoService;
            _logger = logger;
            _dashboardService = dashboardService;
        }


        [HttpGet("{siteName}")] // "api/SiteInfo/{siteName}
        public async Task<IActionResult> GetViews([FromRoute] string siteName)
        {
            int views = await _siteInfoService.GetViews(siteName);
            return Ok(views);
        }
        [HttpPost("{siteName}")]
        public async Task<IActionResult> IncrementViews([FromRoute] string siteName)
        {
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
            bool res = await _siteInfoService.IncrementViews(ipAddress, siteName);
            if (!res)
            {
                _logger.LogInformation("View not incremented for IP: {ipAddress}", ipAddress);
            }
            else
            {
                _logger.LogInformation("View incremented for IP: {ipAddress}", ipAddress);
            }

            return Ok();
        }

        [HttpGet("stats")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var totalCustomers = await _dashboardService.GetTotalCustomers();
            var totalRevenue = await _dashboardService.GetRevenue();
            var totalSales = await _dashboardService.GetTotalSales();
            var siteVisits = await _siteInfoService.GetViews("home"); // "home" is the default siteName for the home page
            var topProducts = await _dashboardService.GetTopProducts();
            var totalEarnings = await _dashboardService.GetTotalEarnings();
            var salesDistribution = await _dashboardService.GetSalesDistribution();

            var stats = new DashboardStat
            {
                Customers = totalCustomers,
                Orders = totalSales,
                Revenue = totalRevenue.ToString("C"),
                SiteVisits = siteVisits,
                TopProducts = topProducts,
                Earnings = totalEarnings,
                SalesDistribution = salesDistribution
            };

            return Ok(stats);

        }
    }
}