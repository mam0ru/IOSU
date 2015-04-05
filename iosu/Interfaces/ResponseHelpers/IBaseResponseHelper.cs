using System.Collections.Generic;

namespace iosu.Interfaces.ResponseHelpers
{
    public interface IBaseResponseHelper<TEntity>
    {
        IList<TEntity> LoadAll();

        TEntity GetEntityById(long? id);

        TEntity SaveOrUpdate(TEntity entity);

        void Delete(long id);
    }
}
