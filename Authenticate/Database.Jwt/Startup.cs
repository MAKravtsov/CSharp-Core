using Database.Jwt.Entities;
using Database.Jwt.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Database.Jwt.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Database.Jwt
{
    public class Startup
    {
        private string AuthorizationConnectionStringName = "AuthorizationDb";
        private IConfiguration _configuration;

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

            // ВАЖНО!!!
            // Identity ипользует отдельную схему аутентификации - "Identity.Application"
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "MyCookie";
                options.LoginPath = "/admin/login";
                options.Cookie.HttpOnly = true;
            });

            services.AddAuthentication(options =>
            {
                // Если у метода используется просто authorize, то по дефолту эта схема
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                // параметры конфигурации схемы аутентификации по дефолту (в данном случае login path будет использоваться из куки - смотри строку 54)
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;

                //options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })

                // ВАЖНО!!!
                // Самостоятельный куки (без Identity)
                /*
                .AddCookie(options =>
                {
                    options.Cookie.Name = "MyCookie";
                    options.LoginPath = "/admin/login";
                })
                */

                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
                {
                    config.SaveToken = true;
                    config.RequireHttpsMetadata = false;

                    byte[] secretBytes = Encoding.UTF8.GetBytes(Constants.Secret_key);

                    var key = new SymmetricSecurityKey(secretBytes);

                    config.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Constants.Issuer,
                        ValidAudience = Constants.Audience,
                        IssuerSigningKey = key
                    };
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // ВАЖНО!!!
            // добавляет токен в хедер при каждом запросе - для jwt
            // важно, что до аутентификации
            app.Use(async (context, next) =>
            {
                if (context.Request.Cookies.TryGetValue("token", out var token))
                {
                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        context.Request.Headers.Add("Authorization", $"Bearer {token}");
                    }
                }

                await next();
            });

            // ВАЖНО!!!
            // перенаправление в случае, если пользователь не аутентифицирован (для jwt)
            app.UseStatusCodePages(async context => {
                await Task.Run(() =>
                {
                    var request = context.HttpContext.Request;
                    var response = context.HttpContext.Response;

                    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
                    // you may also check requests path to do this only for specific methods       
                    // && request.Path.Value.StartsWith("/specificPath")

                    {
                        response.Redirect($"/admin/login?returnurl={request.Path}");
                    }
                });
            });

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
