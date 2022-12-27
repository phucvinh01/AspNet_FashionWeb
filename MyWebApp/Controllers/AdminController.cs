using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebApp.Models;
using PagedList;
using MyWebApp.ViewModel;
using MyWebApp.Identity;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using MyWebApp.Filters;

namespace MyWebApp.Controllers
{
    [AdminFilters]
    public class AdminController : Controller
    {
        // GET: Admin

        //paging

        public ActionResult API()
        {
            return View();    
        }

    
        [HttpPost]
        public ActionResult LogInSuccsesdedAdmin(LogInVM log)
        {
            var _cnn = new Connection();
            var userStore = new userStore(_cnn);
            var userManage = new UserManage(userStore);
            var user = userManage.Find(log.UserName, log.Password);
            if (user != null)
            {
                var authen = HttpContext.GetOwinContext().Authentication;
                var userIdentity = userManage.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authen.SignIn(new AuthenticationProperties(), userIdentity);                
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError("Error", "Dont exits your userName");
                return View("Index");
            }
        }
            public ActionResult Index(int? page)
        {
            ShopDbContext db = new ShopDbContext();


            int pageSize = 8;
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Product> pro1 = null;
            pro1 = db.Products.OrderBy(row => row.Price).ToPagedList(pageIndex, pageSize);
            return View(pro1);
        }


        //insert
        public ActionResult Insert()
        {

            ShopDbContext db = new ShopDbContext();
            //List<Product> pro = db.Products.ToList();
            //Database1Entities1 db = new Database1Entities1();
            //List<Category> pro1 = db.Categories.ToList();  
            ViewBag.Cate = db.Categories.ToList();
                      
            return View();
        }
        [HttpPost]
        public ActionResult InsertNewProduct(Product pro)
        {
            if(ModelState.IsValid)
            {
                ShopDbContext db = new ShopDbContext();
                db.Products.Add(pro);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            else
            {
                return RedirectToAction("index");
            }            
        }


        //delete
        public ActionResult Delete(int? id)
        {
            ShopDbContext db = new ShopDbContext();
            Product pro = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            db.Products.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("index");
        }



        //edit
        public ActionResult Edit(int? id)
        {
            ShopDbContext db = new ShopDbContext();
            Product pro = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            ViewBag.prod = db.Categories.ToList();
            return View(pro);
        }

        [HttpPost]
        public ActionResult EditProduct(Product pro)
        {
            ShopDbContext db = new ShopDbContext();
            Product pronew = db.Products.Where(row => row.ProductID == pro.ProductID).FirstOrDefault();
            pronew.ProductName = pro.ProductName;
            pronew.Price = pro.Price;
            pronew.Description = pro.Description;
            pronew.ImgUrl = pro.ImgUrl;
            pronew.CategoryID= pro.CategoryID;
            db.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult Tops(int? page)
        {
            ShopDbContext db = new ShopDbContext();
            List<Product> pro = new List<Product>();


            int pageSize = 10;
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Product> pro1 = null;
            pro1 = db.Products.Where(row => row.CategoryID == 1).OrderBy(row => row.Price).ToPagedList(pageIndex, pageSize);
            return View(pro1);
        }

        [HttpGet]
        public ActionResult Bottoms(int? page)
        {
            ShopDbContext db = new ShopDbContext();
            List<Product> pro = new List<Product>();


            int pageSize = 10;
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Product> pro1 = null;
            pro1 = db.Products.Where(row => row.CategoryID == 2).OrderBy(row => row.Price).ToPagedList(pageIndex, pageSize);
            return View(pro1);
        }

        [HttpGet]
        public ActionResult Accessories(int? page)
        {
            ShopDbContext db = new ShopDbContext();
            List<Product> pro = new List<Product>();


            int pageSize = 10;
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Product> pro1 = null;
            pro1 = db.Products.Where(row => row.CategoryID == 3).OrderBy(row => row.Price).ToPagedList(pageIndex, pageSize);
            return View(pro1);
        }

        [HttpGet]
        public ActionResult Outerwear(int? page)
        {
            ShopDbContext db = new ShopDbContext();
            List<Product> pro = new List<Product>();


            int pageSize = 10;
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Product> pro1 = null;
            pro1 = db.Products.Where(row => row.CategoryID == 4).OrderBy(row => row.Price).ToPagedList(pageIndex, pageSize);
            return View(pro1);
        }

        [HttpGet]
        public ActionResult Sale(int? page)
        {
            ShopDbContext db = new ShopDbContext();
            List<Product> pro = new List<Product>();


            int pageSize = 10;
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Product> pro1 = null;
            pro1 = db.Products.Where(row => row.CategoryID == 5).OrderBy(row => row.Price).ToPagedList(pageIndex, pageSize);
            return View(pro1);
        }


        //search
        [HttpPost]
        public ActionResult Search(string search = "")
        {
            ShopDbContext db = new ShopDbContext();
            List<Product> pro = db.Products.Where(row => row.ProductName.Contains(search)).ToList();
            return View(pro);
        }
        //get list accout customer

        public ActionResult getCustommers()
        {
            Connection _cnn = new Connection();

            List<AppUser> user = _cnn.Users.ToList();

            return View(user);
        }

    }
}