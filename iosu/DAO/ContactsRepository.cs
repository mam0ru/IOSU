using iosu.Interfaces.DAO;
using iosu.Models;

namespace iosu.DAO
{
    public class ContactsRepository: GenericRepository<Contact>, IContactsRepository
    {
    }
}