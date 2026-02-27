using Microsoft.AspNetCore.Localization;
using OnlineShop.Interfaces;
using OnlineShop.Repositories;
using System.Globalization;
using Serilog;
using OnlineShop.Db.Repositories;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionPstgres = builder.Configuration.GetConnectionString("OnlineShopConnectionPosgres");
string connectionMssql = builder.Configuration.GetConnectionString("OnlineShopConnectionMSSQL");

Log.Logger = new LoggerConfiguration()
        .CreateLogger(); //инициализация глобального логера Serilog с базовой конфигурацией

try //начало блока для обработки ошибок запуска приложения
{
    Log.Information("Starting server..."); //запись информационного сообщения о запуске сервера

    //подключение Serilog в качестве службы логирования по умолчанию, конфигурация настроек читается из файла конфигурации
    builder.Host.UseSerilog((context, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(context.Configuration);
    });

    // Add services to the container.
    builder.Services.AddControllersWithViews().AddViewLocalization();

    builder.Services.AddTransient<IProductsRepository, ProductsDbRepository>();
    builder.Services.AddTransient<ICartsRepository, CartsDbRepository>();
    builder.Services.AddTransient<IOrdersRepository, OrdersDbRepository>();
    builder.Services.AddTransient<IFavoritesRepository, FavoritesDbRepository>();
    builder.Services.AddTransient<IComparisonsRepository, ComparisonsDbRepository>();
    builder.Services.AddSingleton<IRolesRepository, InMemoryRolesRepository>();
    builder.Services.AddSingleton<IUsersRepository, InMemoryUsersRepository>();

    builder.Services.AddLocalization(opt =>
    {
        opt.ResourcesPath = "Resources";
    });

    // добавление английской культуры по умолчанию
    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        var supportedCultures = new[]
        {
            new CultureInfo("en-US"),
            new CultureInfo("ru-RU"),
        };

        options.DefaultRequestCulture = new RequestCulture("en-US");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });

    builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionPstgres)); // Postgres
    //builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionMssql)); // MSSQL

    var app = builder.Build();

    app.UseSerilogRequestLogging(); //замена логирования, используемого по умолчанию в ASP.NET Core, 
                                    //на ведение журнала запросов Serilog
    app.UseHttpsRedirection();
    app.UseRequestLocalization(); // добавление культуры
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "MyArea",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

}
catch (Exception ex) //обработка ошибок запуска: логируется критическая ошибка при аварийном завершении, а затем закрываются и очищаются все логи
{
    Log.Fatal(ex, "server terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
