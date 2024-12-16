using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_api.DTO.Category
{
    public class CreateCategoryDTO
    {

        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool IsPopular { get; set; }
    }
}