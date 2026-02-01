using ProductApp.Application;
using ProductApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddControllers();

var app = builder.Build();
app.UseInfrastructure();
app.MapControllers();


app.Run();

