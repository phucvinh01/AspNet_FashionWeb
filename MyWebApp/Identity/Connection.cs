using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyWebApp.Identity
{

    //protected virtual void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder);
    public class Connection : IdentityDbContext<AppUser>
    {
        public Connection() : base("MyConnectionString") { }

    }
}