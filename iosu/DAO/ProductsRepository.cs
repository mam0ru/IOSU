using System.Collections.Generic;
using iosu.Entities;
using iosu.Interfaces.DAO;

namespace iosu.DAO
{
    public class ProductsRepository: GenericRepository<Product>, IProductRepository
    {
        private readonly IPartnersRepository PartnersRepository;

        public ProductsRepository(IPartnersRepository partnersRepository)
        {
            PartnersRepository = partnersRepository;
        }

        public override Product SaveOrUpdate(Product entity)
        {
            if (entity.Manufacturer == null)
            {
                entity.Manufacturer = PartnersRepository.GetById(entity.ManufacturerId);   
            }
            return base.SaveOrUpdate(entity);
        }

        public override IEnumerable<Product> GetAll()
        {
            IEnumerable<Product> results =  base.GetAll();
            foreach (Product product in results)
            {
                product.Manufacturer = PartnersRepository.GetById(product.ManufacturerId);
            }
            return results;
        }

        public override Product GetById(object id)
        {
            Product product = base.GetById(id);
            product.Manufacturer = PartnersRepository.GetById(product.ManufacturerId);
            return product;
        }
    }
}