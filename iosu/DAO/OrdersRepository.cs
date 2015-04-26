using System.Collections.Generic;
using iosu.DAO.SearchParameters;
using iosu.Interfaces.DAO;
using NHibernate;
using NHibernate.Criterion;
using Order = iosu.Entities.Order;

namespace iosu.DAO
{
    public class OrdersRepository : GenericRepository<Order>, IOrdersRepository
    {
        private readonly IProductRepository ProductRepository;

        private readonly IPartnersRepository PartnersRepository;

        public OrdersRepository(IProductRepository productRepository, IPartnersRepository partnersRepository)
        {
            ProductRepository = productRepository;
            PartnersRepository = partnersRepository;
        }

        public override IEnumerable<Order> GetAll()
        {
            IEnumerable<Order> results = base.GetAll();
            foreach (Order order in results)
            {
                order.Product = ProductRepository.GetById(order.ProductId);
                order.Partner = PartnersRepository.GetById(order.PartnerId);
            }
            return results;
        }

        public override Order GetById(object id)
        {
            var order = base.GetById(id);
            order.Product = ProductRepository.GetById(order.ProductId);
            order.Partner = PartnersRepository.GetById(order.PartnerId);
            return order;
        }

        protected override void AddRestrictions(ICriteria criteria, IBaseSearchParameters parameters)
        {
            criteria.AddOrder(NHibernate.Criterion.Order.Desc("Amount"));
            var param = parameters as OrderSearchParameters;
            if (param != null && param.Amount.HasValue)
            {
                criteria.Add(Restrictions.Ge("Amount", param.Amount.Value));
            }
        }
    }
}