using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebApp.Models
{
    public partial class Shopping
    {
        ShopDbContext db = new ShopDbContext();
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartID";
        public static Shopping GetCart(HttpContextBase context)
        {
            var cart = new Shopping();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        public Shopping GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Product product)
        {
            var cartItem = db.Carts.SingleOrDefault(
                c => c.CartID.ToString() == ShoppingCartId
                && c.ProductID == product.ProductID);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    ProductID = (int)product.ProductID,
                    Product = product ,
                    CartID = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                db.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Count++;
            }
            // Save changes
            db.SaveChanges();

        }

        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = db.Carts.Single(
                cart => cart.CartID == ShoppingCartId
                && cart.RecordID == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    db.Carts.Remove(cartItem);
                }
                // Save changes
                db.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = db.Carts.Where(
                cart => cart.CartID == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                db.Carts.Remove(cartItem);
            }
            // Save changes
            db.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return db.Carts.Where(cart => cart.CartID == ShoppingCartId).ToList();
        }
        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in db.Carts
                          where cartItems.CartID == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in db.Carts
                              where cartItems.CartID == ShoppingCartId
                              select (int?)cartItems.Count * cartItems.Product.Price).Sum();

            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductID = item.ProductID,
                    OrderId = order.OrderID,
                    UnitPrice = (decimal)item.Product.Price,
                    Quantity = item.Count
                };
                // Set the order total of the shopping cart
                orderTotal += (decimal)(item.Count * item.Product.Price);

                db.OrderDetails.Add(orderDetail);

            }
            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Save the order
            db.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.OrderID;
        }

     

        private string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        public void MigrateCart(string userName)
        {
            var shoppingCart = db.Carts.Where(
                c => c.CartID == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartID = userName;
            }
            db.SaveChanges();
        }
    }
}