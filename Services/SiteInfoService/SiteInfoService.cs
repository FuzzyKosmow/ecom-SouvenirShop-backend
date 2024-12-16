using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_api.Models;
using Microsoft.EntityFrameworkCore;
namespace ecommerce_api.Services.SiteInfoService
{
    public class SiteInfoService : ISiteInfoService
    {
        private readonly AppDbContext _context;
        public SiteInfoService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> GetViews(string siteName)
        {
            return await _context.SiteViews.CountAsync(v => v.Page == siteName);
        }
        /// <summary>
        /// If request IP has not visited the site for at least one hour, increment the view count by creating a log of SiteView.
        /// If request IP has visited the site within the last hour, do nothing.
        /// If IP is null, do nothing.
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="siteName"></param>
        /// <returns>
        ///     True if the view count is incremented, false otherwise.
        ///     If IP is null, return false.
        /// </returns>
        public async Task<bool> IncrementViews(string ipAddress, string siteName)
        {

            if (ipAddress == null)
            {
                return false;
            }
            var lastView = await _context.SiteViews.OrderByDescending(v => v.ViewedAt).FirstOrDefaultAsync(v => v.IpAddress == ipAddress && v.Page == siteName);
            if (lastView == null || (DateTime.Now - lastView.ViewedAt).TotalHours >= 1)
            {
                await _context.SiteViews.AddAsync(new SiteView
                {
                    IpAddress = ipAddress,
                    Page = siteName,
                    ViewedAt = DateTime.Now
                });
                await _context.SaveChangesAsync();
                return true;
            }
            return false;









        }
    }
}