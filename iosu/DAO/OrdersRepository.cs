using iosu.Entities;
using iosu.Interfaces.DAO;

namespace iosu.DAO
{
    public class OrdersRepository: GenericRepository<Order>, IOrdersRepository
    {
    }
}