using System;
using System.Net.Http;
using Interfaces.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MiddleWare;
using Shed.CoreKit.WebApi;

namespace ActivityLogger
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorrelationToken();
            services.AddCors();
            services.AddTransient<IActivityLogger, ActivityLoggerImpl>();
            services.AddTransient<HttpClient>();
            
            // локально или в docker
            //services.AddWebApiEndpoints(new WebApiEndpoint<IShoppingCart>(new Uri("http://localhost:5001")));
            services.AddWebApiEndpoints(new WebApiEndpoint<IShoppingCart>(new Uri("http://ShoppingCart")));
            
            services.AddHostedService<Scheduller>();
            services.AddLogging(b => b.AddConsole());
            services.AddRequestLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCorrelationToken();
            app.UseRequestLogging("get");
            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            app.UseWebApiEndpoint<IActivityLogger>();
        }
    }
}