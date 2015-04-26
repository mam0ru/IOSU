using iosu.Entities;

namespace iosu.Interfaces.DAO
{
    public interface IStoreRepository:IRepository<Store>
    {
        Store GetCash();

        Store UpdateCash(decimal cash);
    }
}
