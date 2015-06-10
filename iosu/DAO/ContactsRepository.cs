using iosu.Entities;
using iosu.Interfaces.DAO;

namespace iosu.DAO
{
    public class ContactsRepository: GenericRepository<Contact>, IContactsRepository
    {
        public void Evict(Contact contact)
        {
            Session.Evict(contact);
        }
    }
}