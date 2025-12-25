using Microsoft.AspNetCore.Localization;
using OnlineShop.Interfaces;
using OnlineShop.Repositories;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IProductsRepository, InMemoryProductsRepository>();
builder.Services.AddSingleton<ICartsRepository, InMemoryCartsRepository>();
builder.Services.AddSingleton<IOrdersRepository, InMemoryOrdersRepository>();
builder.Services.AddSingleton<IFavoritesRepository, InMemoryFavoritesRepository>();
builder.Services.AddSingleton<IComparisonsRepository, InMemoryComparisonsRepository>();
builder.Services.AddSingleton<IRolesRepository, InMemoryRolesRepository>();

// добавление английской культуры по умолчанию
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US")
    };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRequestLocalization(); // добавление культурыџ
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
