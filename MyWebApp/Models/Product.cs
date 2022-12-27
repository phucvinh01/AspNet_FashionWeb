using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MyWebApp.Models
{
    public class Product
    {
        [Key]
        public long ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public Nullable<int> Price { get; set; }

        public Nullable<int> CategoryID { get; set; }

        public string Description { get; set; }

        public string ImgUrl { get; set; }

    }
}