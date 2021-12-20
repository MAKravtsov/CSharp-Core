using Microsoft.AspNetCore.Identity;
using System;

namespace TFA.Sms.Data
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
