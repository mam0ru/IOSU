using System;
using System.Collections.Generic;
using iosu.DAO.SearchParameters;
using iosu.Entities;
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

        public void SendToArchive(long partnerId, string tableName, bool isNewArchive)
        {
            ISQLQuery insertQuery = isNewArchive
                ? Session.CreateSQLQuery(String.Format(@"SELECT Id,ProductId,Amount,Price,PartnerId,OrderType,CreatedOn INTO {1} FROM Orders WHERE PartnerId = {0};",
                    partnerId, tableName))
                : Session.CreateSQLQuery(String.Format(@"INSERT INTO {1} SELECT Id,ProductId,Amount,Price,PartnerId,OrderType,CreatedOn  FROM Orders WHERE PartnerId = {0};",
                    partnerId, tableName));
            insertQuery.ExecuteUpdate();
            var deleteQuery = Session.CreateSQLQuery(String.Format(@"DELETE FROM Orders WHERE PartnerId = {0};",
                partnerId));
            deleteQuery.ExecuteUpdate();
        }

        public IList<Order> GetArchivedOrders(string tableName)
        {
            ISQLQuery selectQuery = Session.CreateSQLQuery(String.Format(@"SELECT Id,ProductId,Amount,Price,PartnerId,OrderType,CreatedOn FROM {0};",
                    tableName));
            selectQuery.AddEntity(typeof (Order));
            IList<Order> orders = selectQuery.List<Order>();
            return orders;
        }

        public void RestoreFromArchive(string archiveTableName)
        {
            ISQLQuery insertQuery = Session.CreateSQLQuery(String.Format(@"INSERT INTO Orders SELECT ProductId,Amount,Price,PartnerId,OrderType,CreatedOn FROM {0};",
                archiveTableName));
            insertQuery.ExecuteUpdate();
            ISQLQuery dropQuery = Session.CreateSQLQuery(String.Format(@"DROP TABLE {0};",
                archiveTableName));
            dropQuery.ExecuteUpdate();
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
                if (param.From.HasValue && param.From.Value > DateTime.MinValue && param.To.HasValue &&
                    param.To.Value < DateTime.MaxValue)
                {
                    criteria.Add(Restrictions.Between("CreatedOn", param.From.Value, param.To.Value));
                }
                if (param.PartnerId.HasValue)
                {
                    criteria.Add(Restrictions.Eq("PartnerId", param.PartnerId.Value));
                }
            }
        }
    }
}