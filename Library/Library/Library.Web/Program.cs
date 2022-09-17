using Library.Common.DependancyInjection;

var builder = WebApplication.CreateBuilder(args);

var registrator = DependanciesRegistrator.CreateDependanciesRegistrator();

new Library.Data.DI.DiContainer(builder.Services, registrator).Build();
new Library.Domain.DI.DiContainer(builder.Services, registrator).Build();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
