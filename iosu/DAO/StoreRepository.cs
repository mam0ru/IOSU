using iosu.Entities;
using iosu.Interfaces.DAO;

namespace iosu.DAO
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
    {
        public long StoreId = 1;

        public Store GetCash()
        {
            return GetById(StoreId);
        }

        public Store UpdateCash(decimal cash)
        {
            var store = GetById(StoreId);
            store.Cash = cash;
            SaveOrUpdate(store);
            return store;
        }
    }
}