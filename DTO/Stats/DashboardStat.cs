using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_api.DTO.Stats
{
    public class DashboardStat
    {


        public string Revenue { get; set; }
        public int Orders { get; set; }
        public int Customers { get; set; }
        public int SiteVisits { get; set; }
        public List<SalesDistribution> SalesDistribution { get; set; }
        public List<TotalEarning> Earnings { get; set; }
        public List<TopProduct> TopProducts { get; set; }
    }
}