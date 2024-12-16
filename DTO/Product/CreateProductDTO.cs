using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_api.DTO.Product
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Country { get; set; }
        public string RelatedCity { get; set; }
        public string[] Images { get; set; }
        public List<int> CategoryIds { get; set; }
        public decimal ImportPrice { get; set; }
        // Specifications are key-value pairs

        public DateTime ReleaseDate { get; set; }
    }
}