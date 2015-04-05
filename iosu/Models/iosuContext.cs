﻿using System.Data.Entity;

namespace iosu.Models
{
    public class iosuContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public iosuContext() : base("name=iosuContext")
        {
        }

        public System.Data.Entity.DbSet<iosu.Entities.Partner> Partners { get; set; }

        public System.Data.Entity.DbSet<iosu.Models.Contact> Contacts { get; set; }

        public System.Data.Entity.DbSet<iosu.Entities.Product> Products { get; set; }

        public System.Data.Entity.DbSet<iosu.Entities.Order> Orders { get; set; }
    
    }
}