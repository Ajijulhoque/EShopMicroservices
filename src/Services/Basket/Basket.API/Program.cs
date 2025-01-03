var builder = WebApplication.CreateBuilder(args);

// Add services to the container for DI

var app = builder.Build();

// Configure HTTP request pipeline

app.Run();
