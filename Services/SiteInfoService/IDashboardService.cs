using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_api.DTO.Stats;
namespace ecommerce_api.Services.SiteInfoService
{
    public interface IDashboardService
    {
        Task<List<TopProduct>> GetTopProducts();
        Task<List<TotalEarning>> GetTotalEarnings();
        Task<List<SalesDistribution>> GetSalesDistribution();
        Task<decimal> GetRevenue();
        Task<int> GetTotalSales();
        Task<int> GetTotalProducts();
        Task<int> GetTotalCustomers();

    }
}