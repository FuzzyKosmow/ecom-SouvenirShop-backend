using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_api.DTO.Stats
{
    public class TopProduct
    {
        public string Name { get; set; }
        public int Sales { get; set; }
        public int Stock { get; set; }
        public string Earnings { get; set; }
    }
}