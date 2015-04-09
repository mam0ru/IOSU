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

            References(partner => partner.Contact).Column("ContactId").Unique();
        }  
    }
}