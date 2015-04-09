using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iosu.Entities
{
    public class Product
    {
        public virtual long Id { get; set; }
        [MaxLength(200)]
        public virtual string Name { get; set; }
        [MaxLength(1000)]        
        public virtual string Description { get; set; }
        public virtual long ManufacturerId { get; set; }
        [DisplayName("Unit price")]
        public virtual long UnitPrice { get; set; }
        public virtual long Amount { get; set; }
        public virtual Partner Manufacturer { get; set; }
    }
}