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

            References(order => order.Partner).Column("PartnerId");
            References(order => order.Product).Column("ProductId");//one-to-many implement !
        }
    }
}