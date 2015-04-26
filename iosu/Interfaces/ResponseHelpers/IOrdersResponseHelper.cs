using System.Collections.Generic;
using System.Web.Mvc;
using iosu.Entities;
using iosu.Models;

namespace iosu.Interfaces.ResponseHelpers
{
    public interface IOrdersResponseHelper:IBaseResponseHelper<Order>
    {
        OrderRequestModel GetOrder(long? id);

        void SaveOrder(OrderResponseModel productViewModel);

        void Validate(ModelStateDictionary modelState, OrderResponseModel orderResponse);
        
        OrderRequestModel ConvertResponseObjectToRequestObject(OrderResponseModel orderResponse);
        
        IEnumerable<SelectListItem> GetPartners(OrderType type);

        IEnumerable<SelectListItem> GetProducts(OrderType parse, long? partnerId);

        IEnumerable<Order> GetBigestOrders();
    }
}
