using System.Collections.Generic;
using iosu.Entities;
using iosu.Interfaces.DAO;

namespace iosu.DAO
{
    public class PartnersRepository: GenericRepository<Partner>, IPartnersRepository
    {
        private readonly IContactsRepository ContactsRepository;

        public PartnersRepository(IContactsRepository contactsRepository)
        {
            ContactsRepository = contactsRepository;
        }

//        public override IEnumerable<Partner> GetAll()
//        {
//            IEnumerable<Partner> results = base.GetAll();
//            foreach (Partner partner in results)
//            {
//                partner.Contact = ContactsRepository.GetById(partner.ContactId);
//            }
//            return results;
//        }

//        public override Partner GetById(object id)
//        {
//            Partner result = base.GetById(id);
//            result.Contact = ContactsRepository.GetById(result.ContactId);
//            return result;
//        }

        public override Partner SaveOrUpdate(Partner entity)
        {
            entity.Contact = ContactsRepository.SaveOrUpdate(entity.Contact);
            entity.ContactId = entity.Contact.Id;
            return base.SaveOrUpdate(entity);
        }
    }
}