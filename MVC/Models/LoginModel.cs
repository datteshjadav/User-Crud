using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class LoginModel
    {
    
        [Required(ErrorMessage = "Email needs to be correct")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Please enter a valid email address.")]
        public string? c_email{ get; set;}
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password can't be blank")]
        public string? c_password { get; set; }
        
    }
}