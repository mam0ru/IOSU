using FluentNHibernate.Mapping;
using iosu.Entities;

namespace iosu.Mapping
{
    public class OrderMap: ClassMap<Order>
    {
        public OrderMap()
        {
            Table("Orders");

            Id(order => order.Id);
            Map(order => order.OrderType);
            Map(order => order.Amount);
            Map(order => order.Price);

            Map(order => order.PartnerId).Column("PartnerId");
            Map(order => order.ProductId).Column("ProductId");//one-to-many implement !
        }
    }
}