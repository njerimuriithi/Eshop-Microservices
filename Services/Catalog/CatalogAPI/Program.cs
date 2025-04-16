

using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

//add services to container


builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));    
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));    
});
builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!) ;
}).UseLightweightSessions();

if(builder.Environment.IsDevelopment()) 
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly) ; 
builder.Services.AddExceptionHandler<CustomExceptionHandler>(); 
builder.Services.AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString("Database")!); 

var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
//app.UseExceptionHandler(exceptionHandlerApp =>
//{
//    exceptionHandlerApp.Run(async context =>
//    {
//      var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//        if (exception == null)
//        {
//            return;
//        }

//        var problemDetails = new ProblemDetails
//        {
//            Title = exception.Message,
//            Status = StatusCodes.Status500InternalServerError,
//            Detail= exception.StackTrace
//        };

//        context.Response.StatusCode = StatusCodes.Status500InternalServerError; 
//        context.Response.ContentType = "application/json";  

//        await context.Response.WriteAsJsonAsync(problemDetails);    
//    });
//});


app.Run();
