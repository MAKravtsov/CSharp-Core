using System.ComponentModel.DataAnnotations;

namespace Roles.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не задан username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не задан пароль")]
        public string Password { get; set; }

        public string Role { get; set; }

        [Required(ErrorMessage = "Не задано секретноес слово")]
        public string SecretWord { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
    }
}
