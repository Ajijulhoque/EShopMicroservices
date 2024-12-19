
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

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure HTTP request pipeline
app.MapCarter();

#region Lambda exceptionHandling approach
//app.UseExceptionHandler(exceptionHandlerApp =>
//{
//    exceptionHandlerApp.Run(async context =>
//    {
//        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
//        var exception = exceptionHandlerPathFeature?.Error;

//        if (exception is null)
//        {
//            return;
//        }

//        var problemDetails = new ProblemDetails
//        {
//            Status = StatusCodes.Status500InternalServerError,
//            Title = exception.Message,
//            Detail = exception.StackTrace
//        };

//        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//        logger.LogError(exception, exception.Message);

//        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//        context.Response.ContentType = "application/problem+json";
//        await context.Response.WriteAsJsonAsync(problemDetails);
//    });
//});

#endregion

app.UseExceptionHandler(options => { });

app.Run();
