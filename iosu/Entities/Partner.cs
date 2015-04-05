using iosu.Enums;
using iosu.Models;

namespace iosu.Entities
{
    public class Partner
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual PartnerType PartnerType { get; set; }

        public virtual Contact Contact { get; set; }
    }
}