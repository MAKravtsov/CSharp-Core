using FurnitureShop.Core.Common.Host.Grpc;
using FurnitureShop.Core.Common.Host.Kestrel;
using FurnitureShop.Core.Common.Host.Swagger;
using FurnitureShop.Core.Shop.Domain.Infrastructure;
using FurnitureShop.Core.Shop.Host.Infrastructure;
using FurnitureShop.Core.Shop.Host.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the contsainer.
builder.Services.AddFurnitureShopGrpc();
builder.Services.AddFurnitureShopSwagger();
builder.Services.AddHandlers();
builder.Services.AddMappers();

builder.ConfigureLocalhostKestrel(1112, 1122);

var app = builder.Build();

app.MapGrpcService<FurnitureCatalogService>();
app.MapGrpcService<OrdersService>();

// Configure the HTTP request pipeline.
app.UseFurnitureShopSwagger();

app.Run();