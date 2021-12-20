using Microsoft.AspNetCore.Identity;
using System;

namespace FacebookDemo.Data
{
    public class User : IdentityUser<Guid>
    {
        public User(string userName) : base(userName)
        {
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
