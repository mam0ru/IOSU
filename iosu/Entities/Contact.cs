using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iosu.Entities
{
    public class Contact
    {
        public virtual long Id { get; set; }
        [MaxLength(200)]
        [DisplayName("Aget Full Name")]
        public virtual String AgentFullName { get; set; }
        [MaxLength(500)]        
        public virtual String Adress { get; set; }
        [MaxLength(15)]
        [DisplayName("Phone number")]
        public virtual String PhoneNumber { get; set; }
    }
}