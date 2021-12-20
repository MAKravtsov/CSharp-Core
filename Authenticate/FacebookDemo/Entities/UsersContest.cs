using FacebookDemo.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace FacebookDemo.Entities
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
