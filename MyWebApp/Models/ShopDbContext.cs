using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyWebApp.Models
{
    public class ShopDbContext : DbContext
    {

        public ShopDbContext() : base("MyConnectionString") { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Bag> Shopping { get; set; }

        public DbSet<Cart> Carts { get;set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}