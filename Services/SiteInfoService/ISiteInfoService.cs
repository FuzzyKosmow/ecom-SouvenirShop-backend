using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_api.Services.SiteInfoService
{
    public interface ISiteInfoService
    {
        public Task<int> GetViews(string siteName);
        public Task<bool> IncrementViews(string ipAddress, string siteName);

    }
}