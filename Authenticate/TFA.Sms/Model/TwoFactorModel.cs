using System.ComponentModel.DataAnnotations;
using TFA.Sms.Data;

namespace TFA.Sms.Model
{
    public class TwoFactorModel
    {
        // Валидация для номера телефона
        /*
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        */
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string ReturnUrl { get; set; }

        public string Code { get; set; }
    }
}
