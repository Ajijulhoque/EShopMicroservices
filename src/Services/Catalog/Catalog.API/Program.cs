using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container for DI
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!); //Database
}).UseLightweightSessions();

var app = builder.Build();

// Configure HTTP request pipeline
app.MapCarter();


app.Run();
