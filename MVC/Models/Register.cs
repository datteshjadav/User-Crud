using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class Register
    {
        public int c_userid { get; set; } = 0;

        [Display(Name = "User Name ")]
        [Required(ErrorMessage = "Name Must Be Required.")]
        public string c_username { get; set; } = string.Empty;

        [Display(Name = "Email ")]
        [Required(ErrorMessage = "Email Must Be Required.")]
        public string c_email { get; set; } = string.Empty;

        [Display(Name = "Password")]
        [RegularExpression(@"(?!^[0-9]$)(?!^[a-zA-Z]$)^([a-zA-Z0-9]{6,15})$", ErrorMessage = "at least one digit, one alphabetic character, no special characters, and 6-15 characters in length.")]
        [Required(ErrorMessage = "Password can't be blank!")]
        public string c_password { get; set; } = string.Empty;

        [Display(Name = "CPassword")]
        [Required(ErrorMessage = "Confirm Password can't be Empty")]
        [Compare("c_password", ErrorMessage = "Password & Confirm Password Must Be Same")]
        [DataType(DataType.Password)]
        public string? c_confirmpassword { get; set; }

        [Display(Name = "Gender ")]
        [Required(ErrorMessage = "Select Any One.")]
        public string c_gender { get; set; } = string.Empty;

        [Display(Name = "Hobby")]
        public string c_hobby { get; set; } = string.Empty;

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Please Select an Image.")]
        public string c_image { get; set; } = string.Empty;
    }
}