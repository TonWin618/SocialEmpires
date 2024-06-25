using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using SocialEmpires.Models;
using SocialEmpires.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddRazorPages()
    .AddViewLocalization(options => options.ResourcesPath = "Resources");

services.AddControllers(op => op.Filters.Add<UnitOfWorkFilter>());

var supportedCultures = new[]
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
    var connectionString = builder.Configuration["DbConnectionString"];
    options.UseSqlServer(connectionString);
});

services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // Sign in settings.
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedEmail = false;

    // Token provider settings.
    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultPhoneProvider;

    // User settings.
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-";
    options.User.RequireUniqueEmail = true;
})
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();

services.ConfigureApplicationCookie(cookie =>
{
    cookie.LoginPath = "/Login";
    cookie.LogoutPath = "/Logout";
    cookie.AccessDeniedPath = "/AccessDenied";
    cookie.ExpireTimeSpan = TimeSpan.FromDays(7);
});

services.AddAutoMapper(options =>
{
    options.AddProfile(typeof(AutoMapperProfile));
});

services.AddScoped<CommandService>();
services.AddSingleton<ConfigFileService>();
services.AddScoped<PlayerSaveService>();

var app = builder.Build();

app.UseRequestLocalization();

app.UseAuthentication();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
