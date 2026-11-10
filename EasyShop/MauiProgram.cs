using EasyShop.Data;
using EasyShop.Services;
using EasyShop.ViewModels;
using EasyShop.Views;
using Microsoft.Extensions.Logging;

namespace EasyShop;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>();

        // Camada de Dados (SQL + NoSQL)
        builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
        builder.Services.AddSingleton<IProductRepository, ProductRepository>();

        // Camada de Negócio
        builder.Services.AddSingleton<ICustomerService, CustomerService>();
        builder.Services.AddSingleton<IProductService, ProductService>();

        // Camada de Apresentação
        builder.Services.AddTransient<CustomersViewModel>();
        builder.Services.AddTransient<ProductsViewModel>();
        builder.Services.AddTransient<CustomersPage>();
        builder.Services.AddTransient<ProductsPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
