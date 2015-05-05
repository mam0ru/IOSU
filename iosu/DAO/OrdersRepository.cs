using System;
using iosu.DAO.SearchParameters;
using iosu.Interfaces.DAO;
using NHibernate;
using NHibernate.Criterion;
using Order = iosu.Entities.Order;

namespace iosu.DAO
{
    public class OrdersRepository : GenericRepository<Order>, IOrdersRepository
    {
        public override Order SaveOrUpdate(Order entity)
        {
            entity.CreatedOn = DateTime.UtcNow;
            return base.SaveOrUpdate(entity);
        }

        protected override void AddRestrictions(ICriteria criteria, IBaseSearchParameters parameters)
        {
            criteria.AddOrder(NHibernate.Criterion.Order.Desc("Amount"));
            var param = parameters as OrderSearchParameters;
            if (param != null)
            {
                if (param.Amount.HasValue)
                {
                    criteria.Add(Restrictions.Ge("Amount", param.Amount.Value));
                }
                if (param.From.HasValue && param.From.Value > DateTime.MinValue && param.To.HasValue && param.To.Value < DateTime.MaxValue)
                {
                    criteria.Add(Restrictions.Between("CreatedOn", param.From.Value, param.To.Value));
                }
            }
        }
    }
}