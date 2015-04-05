using FluentNHibernate.Mapping;
using iosu.Models;

namespace iosu.Mapping
{
    public class ContactMap: ClassMap<Contact>
    {
        public ContactMap()
        {
            Table("Contacts");
            Id(contact => contact.Id);
            Map(contact => contact.AgentFullName);
            Map(contact => contact.Adress);
            Map(contact => contact.PhoneNumber);
        }
    }
}