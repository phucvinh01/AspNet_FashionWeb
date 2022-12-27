using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class Cart
    {
        [Key]
        public int RecordID { get; set; }
        public string CartID { get; set; }
        public int Count { get; set; }
        public int ProductID { get; set; }

        public virtual Product Product { get; set; }
        public System.DateTime DateCreated { get; set; }

       
    }
}