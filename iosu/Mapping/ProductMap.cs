using FluentNHibernate.Mapping;
using iosu.Entities;

namespace iosu.Mapping
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Table("Products");

            Id(product => product.Id);
            Map(product => product.Name);
            Map(product => product.Description);
            Map(product => product.Amount);
            Map(product => product.UnitPrice);

            Map(product => product.ManufacturerId).Column("ManufacturerId");
        }
    }
}