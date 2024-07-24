using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using SocialEmpires.Infrastructure.EmailSender;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Infrastructures.NotificationHub;
using SocialEmpires.Models;
using SocialEmpires.Models.Options;
using SocialEmpires.Seeds;
using SocialEmpires.Services;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

//Configure
services.Configure<FileDirectoriesOptions>(builder.Configuration.GetSection("FileDirectories"));
services.Configure<FlashGameConfigOptions>(builder.Configuration.GetSection("FlashGameConfig"));
services.Configure<AzureEmailSenderOptions>(builder.Configuration.GetSection("AzureEmailSender"));

var supportedCultures = new List<CultureInfo>();
foreach (var language in SupportLanguages.List)
{
    supportedCultures.Add(new CultureInfo(language));
}
services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(SupportLanguages.Default);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
});

//Package Services
services.AddControllersWithViews(op => op.Filters.Add<UnitOfWorkFilter>());
services.AddHttpContextAccessor();
services.AddSignalR();
services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
services.AddAutoMapper(options =>
{
    options.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
});
services.AddMemoryCache();
services.AddRazorPages()
    .AddViewLocalization(options => options.ResourcesPath = "Resources");
services.AddLocalization(
    options => options.ResourcesPath = "Resources");
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

services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration["DbConnectionString"];
    options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure(3));
});

//App Services
services.AddScoped<IEmailSender, AzureEmailSender>();
services.AddScoped<CommandService>();
services.AddSingleton<ConfigService>();
services.AddScoped<PlayerSaveService>();

//Data Seed
var seedTypes = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t => string.Equals(t.Namespace, typeof(IDataSeed).Namespace) && t.IsClass && t.IsAssignableTo(typeof(IDataSeed)))
    .ToList();
foreach (var seed in seedTypes)
{
    services.AddTransient(typeof(IDataSeed), seed);
}

var app = builder.Build();
using(var scope = app.Services.CreateScope())
{
    var dataSeeds = scope.ServiceProvider.GetServices<IDataSeed>();
    foreach (var dataSeed in dataSeeds)
    {
        dataSeed.Initialize();
    }
}

//Use
app.UseStaticFiles();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseRequestLocalization();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//Map
app.MapHub<NotificationHub>("/Notification");
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Game}/{action=Index}");

app.Run();
