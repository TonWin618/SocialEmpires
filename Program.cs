using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using SocialEmpires.Infrastructure.EmailSender;
using SocialEmpires.Models;
using SocialEmpires.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;

services.Configure<FileDirectoriesOptions>(config.GetSection("FileDirectories"));

services.AddControllersWithViews(op => op.Filters.Add<UnitOfWorkFilter>());

services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = config["DbConnectionString"];
    options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure(3));
});

services.AddAutoMapper(options =>
{
    options.AddProfile(typeof(AutoMapperProfile));
});

services.AddHttpContextAccessor();

services.AddScoped<CommandService>();
services.AddScoped<ConfigFileService>();
services.AddScoped<PlayerSaveService>();

#region Localization
services.AddRazorPages()
    .AddViewLocalization(options => options.ResourcesPath = "Resources");

var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("zh")
};

services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("zh");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

services.AddLocalization(
    options => options.ResourcesPath = "Resources");
#endregion

#region Identity
services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

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
    cookie.LoginPath = "/Identity/Login";
    cookie.LogoutPath = "/Identity/Logout";
    cookie.AccessDeniedPath = "/Identity/AccessDenied";
    cookie.ExpireTimeSpan = TimeSpan.FromDays(7);
});
#endregion

#region Email Sender
services.AddScoped<IEmailSender, AzureEmailSender>();
services.Configure<AzureEmailSenderOptions>(config.GetSection("AzureEmailSender"));
#endregion

var app = builder.Build();

app.UseRequestLocalization();
app.UseAuthentication();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Game}/{action=Index}");

app.Run();
