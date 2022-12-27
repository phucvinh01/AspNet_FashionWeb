using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebApp.Models;
using MyWebApp.Identity;

namespace MyWebApp.Controllers
{
    [Authorize  ]
    public class CheckoutController : Controller
    {
        ShopDbContext db = new ShopDbContext();
        const string PromoCode = "FREE";
        // GET: Checkout
        public ActionResult AddressAndPayment(/*string name =""*/)
        {
            //Connection _cnn = new Connection();
            //AppUser user = _cnn.Users.Where(row => row.UserName == name).FirstOrDefault();
            //ViewBag.user = user;
            return View();
        }


        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            string name = User.Identity.Name;
            var order = new Order();
            var orderDetail = new OrderDetail();
            TryUpdateModel(order);
            

            Connection _cnn = new Connection();
            AppUser pronew = _cnn.Users.Where(row => row.UserName == name).FirstOrDefault();
            try
            {
                var cart = Shopping.GetCart(this.HttpContext);
                order.UserName = name;
                order.Phone = pronew.PhoneNumber;
                order.Address = pronew.Address;
                order.DateOrder = DateTime.Now;
                order.Total = cart.GetTotal();
                //Save Order
                db.Orders.Add(order);
                db.SaveChanges();
                    //Process the order
                   
                cart.CreateOrder(order);
                return RedirectToAction("Complete",
                     new { id = order.OrderID });
                
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = db.Orders.Any(
                o => o.OrderID == id &&
                o.UserName == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}