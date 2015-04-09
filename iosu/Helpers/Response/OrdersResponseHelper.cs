using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using iosu.Entities;
using iosu.Enums;
using iosu.Interfaces.DAO;
using iosu.Interfaces.ResponseHelpers;
using iosu.Models;

namespace iosu.Helpers.Response
{
    public class OrdersResponseHelper : IOrdersResponseHelper
    {
        private readonly IOrdersRepository OrdersRepository;
        private readonly IPartnersRepository PartnersRepository;
        private readonly IProductRepository ProductRepository;

        public OrdersResponseHelper(IOrdersRepository ordersRepository, IPartnersRepository partnersRepository, IProductRepository productRepository)
        {
            OrdersRepository = ordersRepository;
            PartnersRepository = partnersRepository;
            ProductRepository = productRepository;
        }

        public IList<Order> LoadAll()
        {
            return OrdersRepository.GetAll().ToList();
        }

        public Order GetEntityById(long? id)
        {
            return OrdersRepository.GetById(id);
        }

        public Order SaveOrUpdate(Order entity)
        {
            entity = OrdersRepository.SaveOrUpdate(entity);
            return entity;
        }

        public void Delete(long id)
        {
            OrdersRepository.Delete(id);
        }

        public OrderRequestModel GetOrder(long? id)
        {
            if (!id.HasValue || id == 0)
            {
                return new OrderRequestModel
                {
                    ProductIds =
                        ProductRepository.GetAll()
                            .Select(prod => new SelectListItem
                            {
                                Text = prod.Name,
                                Value = prod.Id.ToString()
                            }),
                    PartnerIds = PartnersRepository.GetAll()
                            .Select(partner => new SelectListItem
                            {
                                Text = partner.Name,
                                Value = partner.Id.ToString()
                            })
                };
            }
            Order product = GetEntityById(id);
            return new OrderRequestModel(product, ProductRepository, PartnersRepository);
        }

        public void SaveOrder(OrderResponseModel orderResponseModel)
        {
            Order order = orderResponseModel.ToEntity(PartnersRepository, ProductRepository);
            if (order.Id == 0)
            {
                if (order.OrderType == OrderType.Sell)
                {
                    order.Product.Amount -= order.Amount;
                }
                else
                {
                    order.Product.Amount += order.Amount;
                }
            }
            else
            {
                var oldOrder = OrdersRepository.GetById(order.Id);
                if (oldOrder.Product.Id == order.Product.Id && order.OrderType == oldOrder.OrderType)
                {
                    if (order.OrderType == OrderType.Sell)
                    {
                        oldOrder.Product.Amount += oldOrder.Amount - order.Amount;
                    }
                    else
                    {
                        oldOrder.Product.Amount -= oldOrder.Amount - order.Amount;
                    }
                }
                oldOrder.OrderType = order.OrderType;
                oldOrder.Partner = order.Partner;
                oldOrder.Price = order.Price;
                oldOrder.Product = ProductRepository.SaveOrUpdate(oldOrder.Product);
                OrdersRepository.SaveOrUpdate(oldOrder);
                return;
            }
            order.Product = ProductRepository.SaveOrUpdate(order.Product);
            OrdersRepository.SaveOrUpdate(order);
        }

        public void Validate(ModelStateDictionary modelState, OrderResponseModel orderResponse)
        {
            var partner = PartnersRepository.GetById(long.Parse(orderResponse.PartnerIds));
            if ((orderResponse.OrderType == OrderType.Buy && partner.PartnerType == PartnerType.Customer) || (orderResponse.OrderType == OrderType.Sell && partner.PartnerType == PartnerType.Manufacturer))
            {
                modelState.AddModelError("PartnerIds","Incorrect set of Partner and Order type");
                modelState.AddModelError("OrderType","Incorrect set of Partner and Order type");
            }
            var product = ProductRepository.GetById(long.Parse(orderResponse.ProductIds));
            if (orderResponse.Amount > product.Amount && orderResponse.OrderType == OrderType.Sell)
            {
                modelState.AddModelError("Amount", "Not enought products in store");                
            }
            if (orderResponse.Price <= 0)
            {
                modelState.AddModelError("Price", "Incorrect price. Price must be great than 0");                
            }
        }

        public OrderRequestModel ConvertResponseObjectToRequestObject(OrderResponseModel orderResponse)
        {
            OrderRequestModel order = GetOrder(0);
            SelectListItem selectedPartner = order.PartnerIds.FirstOrDefault(item => item.Value == orderResponse.PartnerIds);
            if (selectedPartner != null)
            {
                selectedPartner.Selected = true;                
            }
            SelectListItem selectedProduct = order.ProductIds.FirstOrDefault(item => item.Value == orderResponse.ProductIds);
            if (selectedProduct != null)
            {
                selectedProduct.Selected = true;                
            }
            order.OrderType = order.OrderType;
            order.Price = orderResponse.Price;
            order.Amount = orderResponse.Amount;
            return order;
        }
    }
}