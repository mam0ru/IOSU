using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using iosu.Enums;

namespace iosu.Entities
{
    public class Partner
    {
        public virtual long Id { get; set; }

        [MaxLength(200)]
        [MinLength(5)]
        [DisplayName("Partner Name")]
        public virtual string Name { get; set; }
        
        [DisplayName("Partner type")]
        public virtual PartnerType PartnerType { get; set; }

        public virtual long ContactId { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }

        public virtual IEnumerable<Order> Orders { get; set; }

        public virtual bool CantDelete { get; set; }

        public virtual String ArchiveTableName { get; set; }
    }
}