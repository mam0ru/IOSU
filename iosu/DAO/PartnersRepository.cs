using iosu.Interfaces.DAO;
using iosu.Models;

namespace iosu.DAO
{
    public class PartnersRepository: GenericRepository<Partner>, IPartnersRepository
    {
        public PartnersRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}