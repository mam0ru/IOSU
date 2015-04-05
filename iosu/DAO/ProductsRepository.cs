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
            entity.Manufacturer = PartnersRepository.GetById(entity.ManufacturerId);
            return base.SaveOrUpdate(entity);
        }
    }
}