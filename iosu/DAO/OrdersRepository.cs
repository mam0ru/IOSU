using iosu.Interfaces.DAO;
using iosu.Models;

namespace iosu.DAO
{
    public class OrdersRepository: GenericRepository<Order>, IOrdersRepository
    {
        public OrdersRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}