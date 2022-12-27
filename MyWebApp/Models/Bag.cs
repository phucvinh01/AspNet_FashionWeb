using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MyWebApp.Models
{
    public class Bag
    {
        [Key]
        public long ShoppingID { get; set; }
        
        public long ProductID { get; set; }
        public Nullable<double> Total { get; set; }

        public Nullable<int> Quantity { get; set; }

        public string IDCustomer { get; set; }


    }
}