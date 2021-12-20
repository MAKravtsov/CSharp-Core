using System.ComponentModel.DataAnnotations;

namespace FacebookDemo.Model
{
    public class RegisterExternalModel
    {
        [Required(ErrorMessage = "Не задан username")]
        public string UserName { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
    }
}
