using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Cannot be blank")]
        public string UserName { get; set;}
        [Required(ErrorMessage = "Cannot be blank")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Cannot be blank")]
        [Compare("Password",ErrorMessage ="Not Same")]
        public string ConfrimPassword { get; set; }

        [EmailAddress(ErrorMessage ="Invaild Email")]
        public string Email { get; set;  }

        public string Mobile { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Address { get; set; }

        public string City { get; set; }
    }
}