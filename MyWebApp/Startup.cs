﻿using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.EntityFramework;
using MyWebApp.Identity;
[assembly: OwinStartup(typeof(MyWebApp.Startup))]

namespace MyWebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app) 
            {
                app.UseCookieAuthentication(new CookieAuthenticationOptions()
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    LoginPath = new PathString("/admin/index")
                });
            this.createRolesAndUser();
            }

        public void createRolesAndUser()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new Connection()));
            var appConnection = new Connection();
            var appUserStore = new userStore(appConnection);
            var userManager = new UserManage(appUserStore);

            if(!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            if (userManager.FindByName("admin") == null)
            {
                var user = new AppUser();
                user.UserName = "admin";
                user.Email = "nguyenphucvinh1920@gmail.com";
                string password = "admin123";
                var checkUser = userManager.Create(user, password);
                if(checkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "admin");
                }
            }

            if (!roleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);
            }

            if (userManager.FindByName("nanager") == null)
            {
                var user = new AppUser();
                user.UserName = "manager";
                user.Email = "nguyenphucvinh1920@gmail.com";
                string password = "admin123";
                var checkUser = userManager.Create(user, password);
                if (checkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Manager");
                }
            }

            if (!roleManager.RoleExists("Customer"))
            {
                var role = new IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);
            }
        }


        }
    }

