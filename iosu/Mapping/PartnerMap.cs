using FluentNHibernate.Mapping;
using iosu.Entities;

namespace iosu.Mapping
{
    public class PartnerMap: ClassMap<Partner>
    {
        public PartnerMap()
        {
            Table("Partners");

            Id(partner => partner.Id);
            Map(partner => partner.Name).Unique();
            Map(partner => partner.PartnerType);

            Map(partner => partner.ContactId).Column("ContactId");

            References(partner => partner.Contact).Column("ContactId").LazyLoad().Not.Insert().Not.Update();
        }  
    }
}