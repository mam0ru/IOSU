using System.Collections.Generic;
using System.Web.Mvc;
using iosu.Entities;
using iosu.Models;

namespace iosu.Interfaces.ResponseHelpers
{
    public interface IProductResponseHelper: IBaseResponseHelper<Product>
    {
        ProductsCreateViewModel GetProduct(long? id);

        void SaveProduct(ProductsCreateModel productViewModel);

        IEnumerable<Partner> GetReportInfo();
        void Validate(ModelStateDictionary modelState, ProductsCreateModel product);
    }
}
