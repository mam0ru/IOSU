﻿using System.Collections.Generic;
using System.Linq;
using iosu.Interfaces.DAO;
using NHibernate;
using NHibernate.Linq;

namespace iosu.DAO
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ISession Session;

        public GenericRepository()
        {
            Session = NHibernateHelper.OpenSession<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Session.Query<TEntity>().ToList();
        }

        public virtual TEntity GetById(object id)
        {
            return Session.Get<TEntity>(id);
        }

        public virtual TEntity SaveOrUpdate(TEntity entity)
        {
            Session.SaveOrUpdate(entity);
            Session.Flush();
            return entity;
        }

        public virtual void Delete(long id)
        {
            TEntity obj = GetById(id);
            Session.Delete(obj);
            Session.Flush();
        }

        public virtual IEnumerable<TEntity> GetBySearchParameters(IBaseSearchParameters parameters)
        {
            var criteria = Session.CreateCriteria<TEntity>();
            AddRestrictions(criteria, parameters);
            IList<TEntity> result = criteria.List<TEntity>();
            AdditionalActions(result);
            return result;
        }

        protected virtual void AdditionalActions(IList<TEntity> result)
        {
        }

        protected virtual void AddRestrictions(ICriteria criteria, IBaseSearchParameters parameters)
        {
        }
    }
}