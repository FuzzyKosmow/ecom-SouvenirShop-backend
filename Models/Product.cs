using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ecommerce_api.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string Country { get; set; }
        public string RelatedCity { get; set; }
        public decimal? DiscountPrice { get; set; }
        public float Rating { get; set; }
        public bool Availability { get; set; }
        [DefaultValue(0)]
        public decimal ImportPrice { get; set; }

        [DefaultValue(0)]
        public int Stock { get; set; }
        // Country of origin and it's related city

        public List<string> Images { get; set; }

        public string Description { get; set; }

        // Marked by admin or system
        [DefaultValue(false)]
        public bool IsBestSeller { get; set; }
        // Marked by admin
        [DefaultValue(false)]
        public bool IsFeatured { get; set; }
        public DateTime ReleaseDate { get; set; }

        public DateTime CreatedAt { get; set; }
        // Marked by admin or system
        [DefaultValue(false)]
        public bool IsNewArrival { get; set; }
        // Marked by admin or system
        [DefaultValue(false)]
        public bool IsPopular { get; set; }
        //Ignore this property. Used for mapping

        public ICollection<Category> Categories { get; set; } = new List<Category>();



    }



}