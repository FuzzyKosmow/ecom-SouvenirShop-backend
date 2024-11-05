using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_api.Models
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime ValidUntil { get; set; }
        public List<int> ApplicableProductIds { get; set; }
    }

}