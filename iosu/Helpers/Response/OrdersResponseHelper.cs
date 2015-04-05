using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using iosu.Entities;
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
            OrdersRepository.SaveOrUpdate(order);
        }
    }
}