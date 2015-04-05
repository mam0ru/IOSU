using System;
using iosu.Entities;
using iosu.Interfaces.DAO;

namespace iosu.Models
{
    public class OrderResponseModel
    {
        public long Id { get; set; }
        public String ProductIds { get; set; }
        public long Amount { get; set; }
        public long Price { get; set; }
        public String PartnerIds { get; set; }
        public OrderType OrderType { get; set; }

        public Order ToEntity(IPartnersRepository partnersRepository, IProductRepository productRepository)
        {
            var order = new Order
            {
                Id = Id,
                Amount = Amount,
                Price = Price,
                ProductId = long.Parse(ProductIds),
                PartnerId = long.Parse(PartnerIds),
                OrderType = OrderType
            };
            order.Product = productRepository.GetById(order.ProductId);
            order.Partner = partnersRepository.GetById(order.PartnerId);
            return order;
        }
    }
}