using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using iosu.Enums;

namespace iosu.Entities
{
    public class Partner
    {
        public virtual long Id { get; set; }

        [MaxLength(200)]
        public virtual string Name { get; set; }
        [DisplayName("Partner type")]
        public virtual PartnerType PartnerType { get; set; }

        public virtual long ContactId { get; set; }
        public virtual Contact Contact { get; set; }
    }
}