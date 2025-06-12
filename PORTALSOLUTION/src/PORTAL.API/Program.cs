using Microsoft.AspNetCore.HttpOverrides;
using PORTAL.API.Extensions;
using PORTAL.API.Middleware;
using PORTAL.API.Routes;
using Serilog;
using JsonOptions = Microsoft.AspNetCore.Http.Json.JsonOptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
});
builder.Host.UseSerilog();


var app = builder.Build();
//app.MapDefaultEndpoints();
//builder.Host.UseSerilog(); // This sets up the global logger
app.UseSerilogRequestLogging();  // This should be before any other middleware
app.UseCors("MyPolicy");
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();



//app.MapGet("/", () => "Hello World!");
app.RegisterAuthRoutes();
app.RegisterPermissionRoutes();
app.RegisterRoleRoutes();
app.Run();