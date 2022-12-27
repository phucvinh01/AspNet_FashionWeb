using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyWebApp.Identity
{
    public class userStore: UserStore<AppUser>
    {
        public userStore(Connection dbContext) : base(dbContext) { }
    }
}