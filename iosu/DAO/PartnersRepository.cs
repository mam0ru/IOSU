using System.Collections.Generic;
using iosu.Entities;
using iosu.Interfaces.DAO;

namespace iosu.DAO
{
    public class PartnersRepository: GenericRepository<Partner>, IPartnersRepository
    {
        public override IEnumerable<Partner> GetAll()
        {
            IEnumerable<Partner> partners = base.GetAll();
            return partners;
        }
    }
}