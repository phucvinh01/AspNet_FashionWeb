using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebApp.Models;

namespace MyWebApp.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
           ShopDbContext db = new ShopDbContext();
            List<Product> pro = db.Products.ToList();
            List<Category> cate = db.Categories.ToList();
            return View();
        }

        public ActionResult DetailString(int? ID)
        {
            ShopDbContext db = new ShopDbContext();
            Product pro = db.Products.Where(row => row.ProductID == ID).FirstOrDefault();
            return View(pro);
        }
    }
}