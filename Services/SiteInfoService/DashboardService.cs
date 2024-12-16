using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_api.DTO.Stats;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_api.Services.SiteInfoService
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalCustomers()
        {
            return await _context.Users.CountAsync();


        }
        public async Task<int> GetTotalProducts()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<int> GetTotalSales()
        {
            return await _context.Orders.CountAsync();
        }
        // Return top 5 products by sales
        public async Task<List<TopProduct>> GetTopProducts()
        {
            // Fetch the necessary data from the database first
            var productSales = await _context.OrderDetails
                .Include(od => od.Product) // Include the Product entity to access its properties
                .Select(od => new
                {
                    ProductId = od.Product.Id,
                    ProductName = od.Product.Name,
                    od.Quantity,
                    od.Price,

                    od.Product.Stock,
                    ProductPrice = od.Product.Price
                })
                .ToListAsync();

            // Now perform the grouping and calculations in memory
            var topProducts = productSales
                .GroupBy(p => new { p.ProductId, p.ProductName, p.Stock })
                .Select(g => new TopProduct
                {
                    Name = g.Key.ProductName,
                    Sales = g.Sum(x => x.Quantity),
                    Stock = g.Key.Stock,
                    Earnings = g.Sum(x => x.Quantity * x.Price).ToString()
                })
                .OrderByDescending(tp => tp.Sales)
                .Take(5)
                .ToList();

            return topProducts;
        }

        public async Task<decimal> GetRevenue()
        {
            // Calculate revenue by subtracting the import price of sold products from the total price.
            var revenue = await _context.OrderDetails
                .Include(od => od.Product) // Include Product to access ImportPrice
                .Select(od => new
                {
                    TotalPrice = od.Quantity * od.Price, // Total price of the sold products
                    ImportCost = od.Quantity * od.Product.ImportPrice // Import price of the products sold
                })
                .SumAsync(x => x.TotalPrice - x.ImportCost); // Subtract the import cost to get the revenue

            return revenue;
        }

        public async Task<List<TotalEarning>> GetTotalEarnings()
        {
            // Get earnings for the current year, grouped by month
            var currentYear = DateTime.Now.Year;

            // Fetch the necessary data from the database for the current year
            var orders = await _context.Orders
                .Where(o => o.OrderDate.Year == currentYear)
                .Include(o => o.OrderDetails) // Include OrderDetails to calculate the total
                .ThenInclude(od => od.Product) // Include Product to access product data
                .ToListAsync();

            // Create a default list of 12 months with zero earnings
            var earningsByMonth = new List<TotalEarning>();
            string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            // Initialize the earnings for each month to 0
            for (int i = 0; i < 12; i++)
            {
                earningsByMonth.Add(new TotalEarning
                {
                    Name = months[i],
                    Earnings = 0
                });
            }

            // Now, calculate total earnings for each month by iterating over the orders
            foreach (var order in orders)
            {
                var monthIndex = order.OrderDate.Month - 1; // Get the month (1 = Jan, 2 = Feb, etc.)

                // Calculate the total for this order (simulate the Total property)
                var subTotal = order.OrderDetails.Sum(od => od.Quantity * od.Price);
                var total = subTotal + order.ShippingFee + order.Tax - (order.PromoCodeDiscount ?? 0);

                // Add the total to the appropriate month
                earningsByMonth[monthIndex].Earnings += (int)total; // Cast to int, or keep as decimal if needed
            }

            return earningsByMonth;
        }

        // Return by the first category of product. Get the top 4 tags in other detail and order by how total percentage it take.  The rest is "Other"
        public async Task<List<SalesDistribution>> GetSalesDistribution()
        {
            // Group orders by the first category of the product and calculate total sales for each category
            var categorySales = await _context.OrderDetails
                .Include(od => od.Product)
                .ThenInclude(p => p.Categories) // Include product categories
                .Where(od => od.Product.Categories.Any()) // Only consider products with categories
                .GroupBy(od => od.Product.Categories.FirstOrDefault().Name) // Group by the first category name
                .Select(g => new
                {
                    Category = g.Key,
                    TotalSales = g.Sum(od => od.Quantity * od.Price)
                })
                .OrderByDescending(g => g.TotalSales)
                .ToListAsync();

            // Take the top 4 categories
            var topCategories = categorySales.Take(4).Select(c => new SalesDistribution
            {
                Name = c.Category,
                Value = (int)Math.Round(c.TotalSales)
            }).ToList();

            // Calculate the total for the remaining categories and group as "Other"
            var otherSales = categorySales.Skip(4).Sum(c => c.TotalSales);
            if (otherSales > 0)
            {
                topCategories.Add(new SalesDistribution
                {
                    Name = "Other",
                    Value = (int)otherSales
                });
            }

            return topCategories;
        }

    }
}