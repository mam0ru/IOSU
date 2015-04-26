using System.Collections.Generic;
using iosu.Entities;

namespace iosu.Models.View
{
    public class PartnerDetailsViewModel
    {
        public Partner Partner { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Product> Products { get; set; } 
    }
}