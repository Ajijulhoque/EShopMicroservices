using Ordering.API;
using Ordering.Application;
using Ordering.Infrustructure;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container for DI

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

//Configure HTTP request pipeline

app.Run();
