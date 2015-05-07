using System;
using System.Collections.Generic;
using System.Web.Mvc;
using iosu.Entities;
using iosu.Models;
using iosu.Models.View;

namespace iosu.Interfaces.ResponseHelpers
{
    public interface IOrdersResponseHelper:IBaseResponseHelper<Order>
    {
        OrderRequestModel GetOrder(long? id, long? partnerId = null);

        void SaveOrder(OrderResponseModel productViewModel);

        void Validate(ModelStateDictionary modelState, OrderResponseModel orderResponse);
        
        OrderRequestModel ConvertResponseObjectToRequestObject(OrderResponseModel orderResponse);
        
        IEnumerable<SelectListItem> GetPartners(OrderType type);

        IEnumerable<SelectListItem> GetProducts(OrderType parse, long? partnerId);

        IEnumerable<Order> GetBigestOrders();

        IEnumerable<Order> GetReleasedInPeriod(String from, String to);

        IEnumerable<Order> SearchOrdersByProductName(String name);

        IEnumerable<Order> SearchOrdersByPartnerName(String name);

        long? GetProductPrice(long? productId);

        OrderPrintModel GetOrderPrintModel(long id);
    }
}
