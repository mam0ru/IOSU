using System.Collections.Generic;
using System.Linq;
using iosu.Entities;
using iosu.Interfaces.DAO;
using iosu.Interfaces.ResponseHelpers;

namespace iosu.Helpers.Response
{
    public class PartnersResponseHelper: IPartnerResponseHelper
    {
        private readonly IPartnersRepository PartnersRepository;

        public PartnersResponseHelper(IPartnersRepository partnersRepository)
        {
            PartnersRepository = partnersRepository;
        }

        public IList<Partner> LoadAll()
        {
            return PartnersRepository.GetAll().ToList();
        }

        public Partner GetEntityById(long? id)
        {
            return PartnersRepository.GetById(id);
        }

        public Partner SaveOrUpdate(Partner entity)
        {
            entity = PartnersRepository.SaveOrUpdate(entity);
            return entity;
        }

        public void Delete(long id)
        {
            PartnersRepository.Delete(id);
        }
    }
}