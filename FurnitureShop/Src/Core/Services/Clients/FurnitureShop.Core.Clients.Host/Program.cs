using FurnitureShop.Core.Clients.Domain.Infrastructure;
using FurnitureShop.Core.Clients.Host.Infrastructure;
using FurnitureShop.Core.Clients.Host.Services;
using FurnitureShop.Core.Common.Host.Grpc;
using FurnitureShop.Core.Common.Host.Kestrel;
using FurnitureShop.Core.Common.Host.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFurnitureShopGrpc();
builder.Services.AddFurnitureShopSwagger();
builder.Services.AddHandlers();
builder.Services.AddMappers();

builder.ConfigureLocalhostKestrel(1111, 1121);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ClientsService>();

app.UseFurnitureShopSwagger();

app.Run();