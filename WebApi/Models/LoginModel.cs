using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Please enter your registered email")]
        public string?  c_email{ get; set; }=string.Empty;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)$")]
        public string? c_password{get;set;}=string.Empty;
        }
}
