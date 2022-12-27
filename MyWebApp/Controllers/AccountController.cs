using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebApp.Models;
using MyWebApp.ViewModel;
using MyWebApp.Identity;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace MyWebApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _user;
        Connection _cnn = new Connection();
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddNewAccount(RegisterVM acc)
        {

            if (ModelState.IsValid)
            {
                var _cnn = new Connection();
                var userStore = new userStore(_cnn);
                var userManage = new UserManage(userStore);
                var passHash = Crypto.HashPassword(acc.Password);
                var account = new AppUser()
                {
                    Email = acc.Email,
                    UserName = acc.UserName,
                    PasswordHash = passHash,
                    City = acc.City,
                    BirthDay = acc.DateOfBirth,
                    Address = acc.Address,
                    PhoneNumber = acc.Mobile,

                };
                IdentityResult identityResult = userManage.Create(account);

                if (identityResult.Succeeded)
                {
                    userManage.AddToRole(account.Id, "Customer");
                    var authen = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = userManage.CreateIdentity(account, DefaultAuthenticationTypes.ApplicationCookie);
                    authen.SignIn(new AuthenticationProperties(), userIdentity);
                }
                return RedirectToAction("index", "home");

            }
            else
            {
                ModelState.AddModelError("New Error", "Cant Create You Account");
                return View();
            }

        }

        public ActionResult Edit(string name = "")
        {
       
            AppUser user = _cnn.Users.Where(row => row.UserName == name).FirstOrDefault(); 
            ViewBag.user = user;
            return View(user);
        }

        [HttpPost]
        public ActionResult EditProfile(AppUser acc)
        {
            AppUser pronew = _cnn.Users.Where(row => row.UserName == acc.UserName).FirstOrDefault();
            pronew.Address = acc.Address;
            pronew.BirthDay = acc.BirthDay;
            pronew.City = acc.City;
            pronew.Email = acc.Email;
            _cnn.SaveChanges();
            return RedirectToAction("Index","Home");
        }


        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogInSuccsesded(LogInVM log)
        {
            var userStore = new userStore(_cnn);
            var userManage = new UserManage(userStore);
            var user = userManage.Find(log.UserName, log.Password);
            if(user != null)
            {
                var authen = HttpContext.GetOwinContext().Authentication;
                var userIdentity = userManage.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authen.SignIn(new AuthenticationProperties(), userIdentity);
                ViewBag.Name = user.UserName;
                ViewBag.Phone = user.PhoneNumber;
                ViewBag.Address = user.Address;
                ViewBag.Brithday = user.BirthDay;
                ViewBag.Email = user.Email;
                if(userManage.IsInRole(user.Id,"Admin"))
                {
                    return RedirectToAction("index", "Admin");
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Error", "Dont exits your userName");
                return View("login");
            }    
        }

        

        public ActionResult LogOut()
        {
            var authen = HttpContext.GetOwinContext().Authentication;
            authen.SignOut();
            return RedirectToAction("login", "account");
        }

        public ActionResult proFile(string name="")
        {           

            AppUser user = _cnn.Users.Where(row => row.UserName == name).FirstOrDefault();
            return View(user);
        }


        private void MigrateShoppingCart(string UserName)
        {
            // Associate shopping cart items with logged-in user
            var cart = Shopping.GetCart(this.HttpContext);

            cart.MigrateCart(UserName);
            Session[Shopping.CartSessionKey] = UserName;
        }

    }
}