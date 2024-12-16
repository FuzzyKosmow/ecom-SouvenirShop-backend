using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_api.Models
{
    public class SiteView
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public string Page { get; set; }
        public DateTime ViewedAt { get; set; }
    }
}