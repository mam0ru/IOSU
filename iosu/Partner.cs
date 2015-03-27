//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iosu
{
    using System;
    using System.Collections.Generic;
    
    public partial class Partner
    {
        public Partner()
        {
            this.ORDERS = new HashSet<Order>();
            this.PRODUCTS = new HashSet<Product>();
        }
    
        public long Id { get; set; }
        public long ContactId { get; set; }
        public string Name { get; set; }
        public Nullable<long> PartnerType { get; set; }
    
        public virtual Contact Contact { get; set; }
        public virtual ICollection<Order> ORDERS { get; set; }
        public virtual ICollection<Product> PRODUCTS { get; set; }
    }
}
