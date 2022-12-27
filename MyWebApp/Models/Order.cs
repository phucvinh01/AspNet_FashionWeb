using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MyWebApp.Models
{
    public class Order
    {
        [Key]

        public int OrderID { get; set; }
        public string UserName { get; set; }

        public string FristName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public decimal Total { get; set; }
        public System.DateTime DateOrder { get; set; }

        public List<OrderDetail> orderDeitals { get; set; }

    }
}