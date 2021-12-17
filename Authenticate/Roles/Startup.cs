using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Roles.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Roles
{
    public class Startup
    {
        public static string CookieAuthName;
        private string _cookieName;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            CookieAuthName = "CookieAuth";
            _cookieName = "MyCookie";

            services.AddAuthentication(CookieAuthName)
                .AddCookie(CookieAuthName, config =>
                {
                    config.Cookie.Name = _cookieName;
                    config.LoginPath = "/admin/login";
                    config.AccessDeniedPath = "/home/accessdenied";
                });

            // Здесь регулируем policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.AdministratorPolicy, builder =>
                {
                    builder.RequireClaim(CustomClaims.SecretWord, "Secret");
                    builder.RequireClaim(ClaimTypes.Role, Policies.AdministratorPolicy);
                });

                options.AddPolicy(Policies.MangerPolicy, builder =>
                {
                    builder.RequireClaim(CustomClaims.SecretWord, "Secret");
                    builder.RequireAssertion(y => y.User.HasClaim(ClaimTypes.Role, Policies.AdministratorPolicy)
                        || y.User.HasClaim(ClaimTypes.Role, Policies.MangerPolicy));
                });
            });

            services.AddControllersWithViews();
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
