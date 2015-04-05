using System;

namespace iosu.Models
{
    public class Contact
    {
        public virtual long Id { get; set; }
        public virtual String AgentFullName { get; set; }
        public virtual String Adress { get; set; }
        public virtual String PhoneNumber { get; set; }
    }
}