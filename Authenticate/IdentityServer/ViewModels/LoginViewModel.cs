using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Не задан username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не задан пароль")]
        public string Password { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
    }
}
