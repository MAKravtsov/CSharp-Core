using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace FurnitureShop.Core.Common.Host.Kestrel;

public static class ConfigureWebHostBuilderExtensions
{
    public static void ConfigureLocalhostKestrel(
        this WebApplicationBuilder builder,
        int port,
        int portSwagger)
    {
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenLocalhost(port, o => o.Protocols = HttpProtocols.Http2);
            
            if (!builder.Environment.IsDevelopment())
            {
                return;
            }
            
            options.ListenLocalhost(portSwagger, o => o.Protocols = HttpProtocols.Http1AndHttp2);
        });
    }
}