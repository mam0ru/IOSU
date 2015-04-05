using iosu.Entities;
using iosu.Models;

namespace iosu.Interfaces.ResponseHelpers
{
    public interface IProductResponseHelper: IBaseResponseHelper<Product>
    {
        ProductsCreateViewModel GetProduct(long? id);

        void SaveProduct(ProductsCreateModel productViewModel);
    }
}
