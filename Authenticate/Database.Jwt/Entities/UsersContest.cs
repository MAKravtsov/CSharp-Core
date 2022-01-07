using Database.Jwt.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Database.Jwt.Entities
{
    // Можно просто отнаследовать от DbContext, но тогда вся реализация Db лежит на нас
    public class UsersContest : IdentityDbContext<User, Role, Guid>
    {
        public UsersContest(DbContextOptions<UsersContest> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
