using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
//Add services to container
builder.Services.AddApplicationServices(builder.Configuration)
    .AddInfrastuctureServices(builder.Configuration)
    .AddAPIServices(builder.Configuration);
var app = builder.Build();
//HTTP request Pipeline
app.UseApiServices();
if(app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}



app.Run();
