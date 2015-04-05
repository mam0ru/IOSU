using System;
using System.Linq;
using System.Web.Mvc;
using iosu.Entities;
using iosu.Interfaces.DAO;

namespace iosu.Models
{
    public class ProductsCreateModel
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public long UnitPrice { get; set; }
        public long Amount { get; set; }

        public String ManufacturerIds { get; set; }
        public ProductsCreateModel()
        {
        }

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