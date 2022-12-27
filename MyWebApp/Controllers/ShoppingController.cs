using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebApp.Models;

using MyWebApp.ViewModelShopping;

namespace MyWebApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        ShopDbContext db = new ShopDbContext();
        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = Shopping.GetCart(this.HttpContext);

        
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
         
            return View(viewModel);
        }
        //
      
        public ActionResult AddToCart(int? id)
        {

            Product addedAlbum = db.Products.Single(row => row.ProductID == id);

       
            var cart = Shopping.GetCart(this.HttpContext);

            cart.AddToCart(addedAlbum);

      
            return RedirectToAction("Index");
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
           
            var cart = Shopping.GetCart(this.HttpContext);

        
            string albumName = db.Carts
                .Single(item => item.RecordID == id).Product.ProductName;

            int itemCount = cart.RemoveFromCart(id);

          
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(albumName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
     
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = Shopping.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }


    }
}