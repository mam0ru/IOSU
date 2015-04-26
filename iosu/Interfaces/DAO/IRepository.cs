using System.Collections.Generic;

namespace iosu.Interfaces.DAO
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(object id);

        TEntity SaveOrUpdate(TEntity entity);

        void Delete(long id);

        IEnumerable<TEntity> GetBySearchParameters(IBaseSearchParameters parameters);
    }
}