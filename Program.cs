using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddRazorPages()
    .AddViewLocalization(options => options.ResourcesPath = "Resources");
services.AddControllers();

var supportedCultures = new []
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

var app = builder.Build();

app.UseRequestLocalization();
app.UseAuthorization();

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
