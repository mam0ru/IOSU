using System.Collections.Generic;
using iosu.DAO.SearchParameters;
using iosu.Entities;
using iosu.Interfaces.DAO;
using NHibernate;
using NHibernate.Criterion;

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

        public IEnumerable<Partner> GetAllPartnersWithProducts()
        {
            IEnumerable<Partner> results = PartnersRepository.GetAll();
            foreach (Partner partner in results)
            {
                partner.Products =
                    GetBySearchParameters(new PartnerSearchParameters
                    {
                        ParentId = partner.Id
                    });
            }
            return results;
        }

//        public override IEnumerable<Product> GetAll()
//        {
//            IEnumerable<Product> results =  base.GetAll();
//            foreach (Product product in results)
//            {
//                product.Manufacturer = PartnersRepository.GetById(product.ManufacturerId);
//            }
//            return results;
//        }

//        public override Product GetById(object id)
//        {
//            Product product = base.GetById(id);
//            product.Manufacturer = PartnersRepository.GetById(product.ManufacturerId);
//            return product;
//        }

        protected override void AddRestrictions(ICriteria criteria, IBaseSearchParameters parameters)
        {
            var castedParameters = parameters as PartnerSearchParameters;
            if (castedParameters != null)
            {
                if (castedParameters.ParentId.HasValue)
                {
                    criteria.Add(Restrictions.Eq("ManufacturerId", castedParameters.ParentId.Value));
                }
            }
        }
    }
}