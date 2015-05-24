using System;
using System.Globalization;
using iosu.Entities;
using iosu.Interfaces.DAO;
using NHibernate;

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

        public void IncreasProductsPrice(long value, long id)
        {
            ISQLQuery query = Session.CreateSQLQuery(String.Format("UPDATE Products SET [UnitPrice] = [UnitPrice] + [UnitPrice]*{0} WHERE [ManufacturerId] = {1};", ((double)value / 100).ToString(CultureInfo.GetCultureInfo("en-GB")), id));
            query.ExecuteUpdate();
        }

        public void ReducProductsPrice(long value, long id)
        {
            ISQLQuery query = Session.CreateSQLQuery(String.Format("UPDATE Products SET [UnitPrice] = [UnitPrice] - [UnitPrice]*{0} WHERE [ManufacturerId] = {1};", ((double)value / 100).ToString(CultureInfo.GetCultureInfo("en-GB")), id));
            query.ExecuteUpdate();
        }
    }
}