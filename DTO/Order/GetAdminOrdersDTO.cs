using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_api.Models;
namespace ecommerce_api.DTO.Order
{
    public class GetAdminOrdersDTO
    {
        public List<Models.Order> Orders { get; set; }
        public int TotalOrders { get; set; }
    }
}