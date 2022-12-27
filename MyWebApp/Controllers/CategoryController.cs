    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebApp.Models;
using PagedList;

namespace MyWebApp.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category


        //category
        [HttpGet]
        public ActionResult All(int? page)
        {
            ShopDbContext db = new ShopDbContext();
           

            int pageSize = 10;
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Product> pro1 = null;
            pro1 = db.Products.OrderBy(row => row.Price).ToPagedList(pageIndex, pageSize); 
            return View(pro1);
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
        public ActionResult Search(string search="")
        {
            ShopDbContext db = new ShopDbContext();
            List<Product> pro = db.Products.Where(row => row.ProductName.Contains(search)).ToList();
            return View(pro);
        }


        //sort

        [HttpGet]
        public ActionResult SortOrderBy(int? page)
        {
            ShopDbContext db = new ShopDbContext();
            List<Product> pro = new List<Product>();


            int pageSize = 10;
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Product> pro1 = null;
            pro1 = db.Products.OrderBy(row => row.Price).ToPagedList(pageIndex, pageSize);
            return View(pro1);
        }

        [HttpGet]
        public ActionResult SortOrderByDes(int? page)
        {
            ShopDbContext db = new ShopDbContext();
            List<Product> pro = new List<Product>();


            int pageSize = 10;
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Product> pro1 = null;
            pro1 = db.Products.OrderByDescending(row => row.Price).ToPagedList(pageIndex, pageSize);
            return View(pro1);
        }




    }
}