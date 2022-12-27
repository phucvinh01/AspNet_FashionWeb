using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.ViewModel
{
    public class LogInVM
    {
        [Required(ErrorMessage = "Cannot be blank")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Cannot be blank")]
        public string Password { get; set; }

   
    }
}