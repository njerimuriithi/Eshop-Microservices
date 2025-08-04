using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
//Add services to container
builder.Services.AddApplicationServices()
    .AddInfrastuctureServices(builder.Configuration)
    .AddAPIServices();
var app = builder.Build();
//HTTP request Pipeline

app.Run();
