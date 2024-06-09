using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialEmpires.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddRazorPages()
    .AddViewLocalization(options => options.ResourcesPath = "Resources");
services.AddControllers(op => op.Filters.Add<UnitOfWorkFilter>());

var supportedCultures = new []
{
    new CultureInfo("en"),
    new CultureInfo("zh")
};

services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("zh");
    options.SupportedCultures = supportedCultures;
});

services.AddLocalization(
    options => options.ResourcesPath = "Resources");

services.AddDbContext<AppDbContext>(options =>
{
    var dbstr = "Data Source = Models/data.db";
    options.UseSqlite(dbstr, sqliteOptionsAction =>
    {
        sqliteOptionsAction.MigrationsAssembly(typeof(Program).Assembly.GetName().Name);
    });
});

//services.Configure<MvcOptions>(options =>
//{
//    options.Filters.Add<UnitOfWorkFilter>();
//});

services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    options.User.RequireUniqueEmail = false;
})
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();

services.ConfigureApplicationCookie(cookie =>
{
    cookie.LoginPath = "/Login";
    cookie.LogoutPath = "/Logout";
    cookie.AccessDeniedPath = "/AccessDenied";
    cookie.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    // cookie.SlidingExpiration = true;
});

var app = builder.Build();

app.UseRequestLocalization();
app.UseAuthorization();
app.UseAuthentication();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllers();
app.MapRazorPages();

app.Run();
