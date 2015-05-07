using iosu.Interfaces.DAO;

namespace iosu.DAO.SearchParameters
{
    public class PartnerSearchParameters: IBaseSearchParameters
    {
        public long? ParentId { get; set; }
    }
}