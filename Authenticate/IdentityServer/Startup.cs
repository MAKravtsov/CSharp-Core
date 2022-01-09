using IdentityServer.Data;
using IdentityServer.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IdentityServer4.AspNetIdentity;
using IdentityServer.Infrastructure;

namespace IdentityServer
{
    public class Startup
    {
        private IConfiguration _configuration;
        private string AuthorizationConnectionStringName = "AuthorizationDb";

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration.GetConnectionString(AuthorizationConnectionStringName);

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
                .AddEntityFrameworkStores<UsersContest>();

            services.AddIdentityServer(config =>
            {
                config.UserInteraction.LoginUrl = "/Auth/Login";
            })
                // связь entityframewor юзера с identityServer юзером
                .AddAspNetIdentity<User>()
                .AddInMemoryClients(Configuration.GetClients())
                .AddInMemoryApiResources(Configuration.GetApiResources())
                .AddInMemoryIdentityResources(Configuration.GetIdentityResoures())
                .AddProfileService<ProfileService>()
                .AddDeveloperSigningCredential();

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

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
