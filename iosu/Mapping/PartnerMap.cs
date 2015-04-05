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
            Map(partner => partner.Name);
            Map(partner => partner.PartnerType);

            References(partner => partner.Contact).Column("ContactId").Cascade.All().Unique();
        }  
    }
}