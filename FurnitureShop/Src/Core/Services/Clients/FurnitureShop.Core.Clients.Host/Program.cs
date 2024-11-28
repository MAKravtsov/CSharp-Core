using FurnitureShop.Core.Clients.Domain.Infrastructure;
using FurnitureShop.Core.Clients.Host.Infrastructure;
using FurnitureShop.Core.Clients.Host.Services;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHandlers();
builder.Services.AddMappers();
builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ClientsService>();

app.Run();