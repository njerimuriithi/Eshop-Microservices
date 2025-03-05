using BuildingBlocks.Behaviors;

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

var app = builder.Build();

app.MapCarter();


app.Run();
