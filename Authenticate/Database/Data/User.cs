using Microsoft.AspNetCore.Identity;
using System;

namespace Database.Data
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
