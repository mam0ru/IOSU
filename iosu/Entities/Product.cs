namespace iosu.Entities
{
    public class Product
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual long ManufacturerId { get; set; }
        public virtual long UnitPrice { get; set; }
        public virtual long Amount { get; set; }

        public virtual Partner Manufacturer { get; set; }
    }
}