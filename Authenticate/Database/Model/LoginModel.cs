using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не задан username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не задан пароль")]
        public string Password { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
    }
}
