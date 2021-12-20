using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using TFA.Sms.Data;
using TFA.Sms.Entities;
using TFA.Sms.Infrastructure;
using TFA.Sms.Configuration;
using TFA.Sms.Services.Implementations;
using TFA.Sms.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace TFA.Sms
{
    public class Startup
    {
        public static string CookieAuthName;
        private string AuthorizationConnectionStringName = "AuthorizationDb";
        private string _cookieName;
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            CookieAuthName = "CookieAuth";
            _cookieName = "MyCookie";

            var connectionString = _configuration.GetConnectionString(AuthorizationConnectionStringName);

            SMSoptions.Configure(_configuration["SmsOptions:SMSAccountIdentification"],
                _configuration["SmsOptions:SMSAccountPassword"],
                _configuration["SmsOptions:SMSAccountFrom"]);

            services
                .AddDbContext<UsersContest>(options =>
                {
                    options.UseSqlServer(connectionString);
                })
                // Добавление Microsoft Identity
                .AddIdentity<User, Role>(config =>
                {
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                    config.Password.RequireLowercase = false;
                })
                // Обязательно надо! Иначе не поймет какой контекст использовать
                .AddEntityFrameworkStores<UsersContest>()
                .AddDefaultTokenProviders();

            // Нужно, чтобы сконфигурировать куки
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = _cookieName;
                config.LoginPath = "/admin/login";
                config.AccessDeniedPath = "/admin/accessdenied";
            });

            // Здесь регулируем policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.AdministratorPolicy, builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, Policies.AdministratorPolicy);
                });

                options.AddPolicy(Policies.MangerPolicy, builder =>
                {
                    builder.RequireAssertion(y => y.User.HasClaim(ClaimTypes.Role, Policies.AdministratorPolicy)
                        || y.User.HasClaim(ClaimTypes.Role, Policies.MangerPolicy));
                });
            });

            services.AddControllersWithViews();

            services.AddSingleton<ISmsService, SmsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // who are you?
            app.UseAuthentication();

            // are you allowed?
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
