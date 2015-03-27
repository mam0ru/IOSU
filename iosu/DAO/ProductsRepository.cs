using iosu.Interfaces.DAO;
using iosu.Models;

namespace iosu.DAO
{
    public class ProductsRepository: GenericRepository<Product>, IProductRepository
    {
        public ProductsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}