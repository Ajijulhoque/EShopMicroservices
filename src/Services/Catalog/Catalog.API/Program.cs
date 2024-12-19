
var builder = WebApplication.CreateBuilder(args);

// Add services to the container for DI
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!); //Database
}).UseLightweightSessions();

var app = builder.Build();

// Configure HTTP request pipeline
app.MapCarter();


app.Run();
