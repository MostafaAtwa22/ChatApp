using System.ComponentModel.DataAnnotations;

namespace ChatApp.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RemeberMe { get; set; }
    }
}
