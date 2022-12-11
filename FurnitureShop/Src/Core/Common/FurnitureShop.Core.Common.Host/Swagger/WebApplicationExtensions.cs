using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace FurnitureShop.Core.Common.Host.Swagger;

public static class WebApplicationExtensions
{
    public static void UseFurnitureShopSwagger(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return;
        }

        app.UseSwagger();
        app.UseSwaggerUI();
    }
}