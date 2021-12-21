using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VKontakte.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не задан username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не задан пароль")]
        public string Password { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
        public IEnumerable<AuthenticationScheme> ExternalSystems { get; internal set; }
    }
}
