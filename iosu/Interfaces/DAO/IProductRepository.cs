using System.Collections.Generic;
using iosu.Entities;

namespace iosu.Interfaces.DAO
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Partner> GetAllPartnersWithProducts();
    }
}
