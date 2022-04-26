using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dia_Supermarket.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter email address.")]
        public string email_address { get; set; }


        [Required(ErrorMessage = "Please enter password.")]
        public string password { get; set; }
    }
}