using FurnitureShop.Core.Shop.Domain.Infrastructure;
using FurnitureShop.Core.Shop.Host.Infrastructure;
using FurnitureShop.Core.Shop.Host.Services;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the contsainer.
builder.Services.AddHandlers();
builder.Services.AddMappers();
builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<FurnitureCatalogService>();
app.MapGrpcService<OrdersService>();

app.Run();