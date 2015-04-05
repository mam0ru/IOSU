using iosu.Entities;
using iosu.Models;

namespace iosu.Interfaces.ResponseHelpers
{
    public interface IOrdersResponseHelper:IBaseResponseHelper<Order>
    {
        OrderRequestModel GetOrder(long? id);

        void SaveOrder(OrderResponseModel productViewModel);
    }
}
