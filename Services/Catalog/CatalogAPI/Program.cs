

var builder = WebApplication.CreateBuilder(args);

//add services to container


builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));    
});
builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!) ;
}).UseLightweightSessions();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly) ; 
builder.Services.AddExceptionHandler<CustomExceptionHandler>(); 

var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(options => { });
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
