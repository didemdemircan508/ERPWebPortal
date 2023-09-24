using ERPWebPortal.Data.Abstract;
using ERPWebPortal.Data.Concrete.Json;
using ERPWebPortal.Services.Abstract;
using ERPWebPortal.Services.Concrete;
using ERPWebPortal.Shared.Abstract;
using ERPWebPortal.Shared.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPrdOrderService,PrdOrderManager>();
builder.Services.AddScoped<IReportService, ReportManager>();
builder.Services.AddScoped<IFaultService, FaultManager>();
builder.Services.AddScoped<IPrdOrderRepository, JsonPrdOrderRepository>();
builder.Services.AddScoped<IFaultRepository, JsonFaultRepository>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();



app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
