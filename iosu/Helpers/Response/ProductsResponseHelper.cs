using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using iosu.Entities;
using iosu.Enums;
using iosu.Interfaces.DAO;
using iosu.Interfaces.ResponseHelpers;
using iosu.Models;

namespace iosu.Helpers.Response
{
    public class ProductsResponseHelper : IProductResponseHelper
    {
        private readonly IProductRepository ProductRepository;

        private readonly IPartnersRepository PartnersRepository;

        public ProductsResponseHelper(IProductRepository productRepository, IPartnersRepository partnersRepository)
        {
            ProductRepository = productRepository;
            PartnersRepository = partnersRepository;
        }

        public IList<Product> LoadAll()
        {
            return ProductRepository.GetAll().ToList();
        }

        public Product GetEntityById(long? id)
        {
            return ProductRepository.GetById(id);
        }

        public Product SaveOrUpdate(Product entity)
        {
            entity.Manufacturer = PartnersRepository.GetById(entity.ManufacturerId);
            return ProductRepository.SaveOrUpdate(entity);
        }

        public void Delete(long id)
        {
            ProductRepository.Delete(id);
        }

        public ProductsCreateViewModel GetProduct(long? id)
        {
            if (!id.HasValue || id.Value == 0)
            {
                return new ProductsCreateViewModel
                {
                    ManufacturerIds = PartnersRepository
                        .GetAll()
                        .Where(partner => partner.PartnerType == PartnerType.Manufacturer)
                        .Select(partner => new SelectListItem
                        {
                            Value = partner.Id.ToString(),
                            Text = partner.Name
                        })
                };
            }
            var product = GetEntityById(id);
            return new ProductsCreateViewModel(product, PartnersRepository);
        }

        public void SaveProduct(ProductsCreateModel productViewModel)
        {
            Product product = productViewModel.ToEntity();
            ProductRepository.SaveOrUpdate(product);
        }
    }
}