using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using VKontakte.Data;
using VKontakte.Entities;
using VKontakte.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;

namespace VKontakte
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

            services.AddAuthentication()
                .AddFacebook(config =>
                {
                    config.AppId = _configuration["Authentication:Facebook:AppId"];
                    config.AppSecret = _configuration["Authentication:Facebook:AppSecret"];
                })
                .AddOAuth("VK", "VKontakte", config =>
                {
                    config.ClientId = _configuration["Authentication:VK:AppId"];
                    config.ClientSecret = _configuration["Authentication:VK:AppSecret"];
                    config.ClaimsIssuer = Constants.Issuer;
                    config.CallbackPath = new PathString(Constants.CallBackPath);
                    config.AuthorizationEndpoint = Constants.AuthorizationEndpoint;
                    config.TokenEndpoint = Constants.TokenEndpoint;
                    config.Scope.Add("email");
                    config.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "user_id");
                    config.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                    config.SaveTokens = true;
                    config.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents()
                    {
                        OnCreatingTicket = context =>
                        {
                            context.RunClaimActions(context.TokenResponse.Response.RootElement);
                            return Task.CompletedTask;
                        },
                        OnRemoteFailure = OnFailure
                    };
                });

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
        }

        private Task OnFailure(RemoteFailureContext arg)
        {
            return Task.CompletedTask;
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
