using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using iosu.Entities;

namespace iosu.Models
{
    public class ProductsCreateModel
    {
        public long Id { get; set; }
        [MaxLength(200)]
        public String Name { get; set; }
        [MaxLength(1000)]        
        public String Description { get; set; }
        [DisplayName("Unit price")]        
        public long UnitPrice { get; set; }
        public long Amount { get; set; }
        public String ManufacturerIds { get; set; }

        public Product ToEntity()
        {
            return new Product
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Amount = Amount,
                ManufacturerId = long.Parse(ManufacturerIds),
                UnitPrice = UnitPrice
            };
        }
    }
}