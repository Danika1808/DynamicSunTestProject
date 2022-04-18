using Application.Services;
using Application.Services.ArchiveServices;
using Application.Services.ExcelConverterService;
using Application.Services.ExcelFileReaderService;
using Application.Services.WeatherServices;
using Domain.Users;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using SunTestProject.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{ 
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbConnection")); 
}, ServiceLifetime.Scoped);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IExcelFileReader, ExcelFileReader>();
builder.Services.AddScoped<IExcelConverter, ExcelConverter>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IArchiveService, ArchiveService>();
builder.Services.AddMapper();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await RoleInitializer.InitializeAsync(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
