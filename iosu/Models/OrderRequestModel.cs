using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using iosu.Entities;
using iosu.Interfaces.DAO;

namespace iosu.Models
{
    public class OrderRequestModel
    {
        public long Id { get; set; }
        public IEnumerable<SelectListItem> ProductIds { get; set; }
        public long Amount { get; set; }
        public long Price { get; set; }
        public IEnumerable<SelectListItem> PartnerIds { get; set; }
        public OrderType OrderType { get; set; }

        public OrderRequestModel()
        {
        }

        public OrderRequestModel(Order order, IProductRepository productRepository, IPartnersRepository partnersRepository)
        {
            Id = order.Id;
            Amount = order.Amount;
            Price = order.Price;
            ProductIds =
                productRepository.GetAll()
                    .Select(product => new SelectListItem
                    {
                        Text = product.Name,
                        Value = product.Id.ToString(),
                        Selected = product.Id == order.ProductId
                    });
            PartnerIds = partnersRepository.GetAll()
                    .Select(partner => new SelectListItem
                    {
                        Text = partner.Name,
                        Value = partner.Id.ToString(),
                        Selected = partner.Id == order.PartnerId
                    });
            OrderType = order.OrderType;
            Amount = order.Amount;
        }
    }
}