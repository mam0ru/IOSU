using System;

namespace iosu.Entities
{
    public class Order
    {
        public virtual long Id { get; set; }
        public virtual long ProductId { get; set; }
        public virtual long Amount { get; set; }
        public virtual long Price { get; set; }
        public virtual long PartnerId { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual OrderType OrderType { get; set; }

        public virtual Partner Partner { get; set; }
        public virtual Product Product { get; set; }
        public virtual bool CantDelete { get; set; }
    }

    public enum OrderType
    {
        Buy = 0,
        Sell = 1
    }
}