using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_api.DTO.Product
{
    public class DestinationDTO
    {
        // Country name with a list of related cities.
        public string Country { get; set; }
        public List<string> Cities { get; set; }
    }
}