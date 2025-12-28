using Microsoft.AspNetCore.Localization;
using OnlineShop.Interfaces;
using OnlineShop.Repositories;
using System.Globalization;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

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

    app.UseSerilogRequestLogging(); //замена логирования, используемого по умолчанию в ASP.NET Core, 
                                    //на ведение журнала запросов Serilog
    app.UseHttpsRedirection();
    app.UseRequestLocalization(); // добавление культурыЫ
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();

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
