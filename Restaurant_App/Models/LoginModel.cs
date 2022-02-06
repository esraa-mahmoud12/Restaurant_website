using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant_App.Models
{
    public class LoginModel
    {
        

        [Required(ErrorMessage = "Email required")]
        [Display(Name = "Email:")]
        public String Email { get; set; }

        [Required(ErrorMessage = "password required")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }


}