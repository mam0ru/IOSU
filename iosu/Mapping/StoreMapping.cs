using FluentNHibernate.Mapping;
using iosu.Entities;

namespace iosu.Mapping
{
    public class StoreMapping : ClassMap<Store>
    {
        public StoreMapping()
        {
            Table("Store");

            Id(store => store.Id);
            Map(store => store.Cash).Column("Cash");
        }
    }
}