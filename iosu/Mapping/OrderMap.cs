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
            Map(order => order.CreatedOn);

            Map(order => order.PartnerId).Column("PartnerId");
            Map(order => order.ProductId).Column("ProductId");//one-to-many implement !

            References(order => order.Product).Column("ProductId").LazyLoad().Cascade.None().Not.Insert().Not.Update();
            References(order => order.Partner).Column("PartnerId").LazyLoad().Cascade.None().Not.Insert().Not.Update();
        }
    }
}