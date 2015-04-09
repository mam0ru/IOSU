using iosu.Entities;
using iosu.Interfaces.DAO;

namespace iosu.DAO
{
    public class ProductsRepository: GenericRepository<Product>, IProductRepository
    {
    }
}