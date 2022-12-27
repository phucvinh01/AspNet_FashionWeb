using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyWebApp.Models;

namespace MyWebApp.APIController
{
    public class ProductController : ApiController
    {
        ShopDbContext db = new ShopDbContext();
        public List<Product> GetProducts()
        {
            List<Product> pro = db.Products.ToList();
            return pro;
        }

        public Product GetID(long id)
        {
            Product pro = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            return pro;
        }

        public void PostProduct(Product pro)
        {
        
            db.Products.Add(pro);
            db.SaveChanges();

        }
        public void PutProduct(Product pro)
        {
            Product product = db.Products.Where(row => row.ProductID == pro.ProductID).FirstOrDefault();
            product.ProductName = pro.ProductName;
            product.Price = pro.Price;
            product.ImgUrl = pro.ImgUrl;
            product.Description = pro.Description;
            db.SaveChanges();
        }

        public void DeleteProduct(long id)
        {
            Product product = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            db.Products.Remove(product);
            db.SaveChanges();
        }

    }
}
