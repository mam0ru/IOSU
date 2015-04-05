using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using iosu.Entities;
using iosu.Interfaces.DAO;

namespace iosu.Models
{
    public class ProductsCreateViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<SelectListItem> ManufacturerIds { get; set; }
        public long UnitPrice { get; set; }
        public long Amount { get; set; }

        public String ManufacturerId { get; set; }
        public ProductsCreateViewModel()
        {
        }

        public ProductsCreateViewModel(Product product, IPartnersRepository partnersRepository)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            ManufacturerIds =
                partnersRepository.GetAll()
                    .Where(partner => partner.PartnerType == 0)
                    .Select(partner => new SelectListItem
                    {
                        Text = partner.Name,
                        Value = partner.Id.ToString(),
                        Selected = partner.Id == product.ManufacturerId
                    });
            UnitPrice = product.UnitPrice;
            Amount = product.Amount;
        }

        public Product ToEntity()
        {
            return new Product
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Amount = Amount,
                ManufacturerId = ManufacturerIds.Where(item => item.Selected).Select(item => long.Parse(item.Value)).FirstOrDefault(),
                UnitPrice = UnitPrice
            };
        }
    }
}