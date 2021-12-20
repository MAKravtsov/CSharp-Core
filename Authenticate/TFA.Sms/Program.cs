using TFA.Sms.Data;
using TFA.Sms.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TFA.Sms
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Динамическое создания первого пользователя
            using (var scope = host.Services.CreateScope())
            {
                DatabaseInitializer.Init(scope.ServiceProvider).Wait();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    /// <summary>
    /// Класс для создания первого пользователя
    /// </summary>
    public class DatabaseInitializer
    {
        public static async Task Init(IServiceProvider provider)
        {
            var userManager = provider.GetRequiredService<UserManager<User>>();

            if (userManager.Users.Any())
                return;

            var user = new User
            {
                UserName = "ma_kravtsov",
                FirstName = "Maksim",
                LastName = "Kravtsov",
                PhoneNumber = "+79017674475",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
            };

            var result = await userManager.CreateAsync(user, "123123");

            if (result.Succeeded)
            {
                await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator"));
            }
        }
    }
}
