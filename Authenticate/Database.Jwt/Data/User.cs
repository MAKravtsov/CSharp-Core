using Microsoft.AspNetCore.Identity;
using System;

namespace Database.Jwt.Data
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
