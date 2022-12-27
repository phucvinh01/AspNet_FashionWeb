using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
namespace MyWebApp.Identity
{
    public class UserManage : UserManager<AppUser>
    {
        public UserManage(IUserStore<AppUser> store) : base(store) { }
    }
}