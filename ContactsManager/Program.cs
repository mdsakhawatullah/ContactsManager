using ContactsManager.StartupExtensions;
using Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);


//serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration
   .ReadFrom.Configuration(context.Configuration) //read configuration settings from built-in IConfiguration
   .ReadFrom.Services(services); //read out current app's services and make them available to serilog

});

builder.Services.ConfigureServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

//app.UseHttpLogging();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
