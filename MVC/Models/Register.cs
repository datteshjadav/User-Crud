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
        [Required(ErrorMessage = "Name Must Be Required.")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)$")]
        public string c_password { get; set; } = string.Empty;

        [Display(Name = "Gender ")]
        [Required(ErrorMessage = "Select Any One.")]
        public string c_gender { get; set; } = string.Empty;

        [Display(Name = "Hobby")]
        public string? c_hobby { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Please Select an Image.")]
        public string c_image { get; set; } = string.Empty;
    }
}