using iosu.Entities;

namespace iosu.Interfaces.DAO
{
    public interface IPartnersRepository : IRepository<Partner>
    {
        void IncreasProductsPrice(long value, long id);
        void ReducProductsPrice(long value, long id);
    }
}
