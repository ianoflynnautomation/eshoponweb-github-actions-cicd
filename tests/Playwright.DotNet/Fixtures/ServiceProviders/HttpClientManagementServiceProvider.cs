

using BlazorAdmin.Services;
using BlazorShared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Playwright.DotNet.Fixtures.ServiceProviders;


public class HttpClientManagementServiceProvider : IServiceProvider
{
    private readonly ServiceProvider _serviceProvider;

    public HttpClientManagementServiceProvider(string baseAddress)
    {
        var services = new ServiceCollection();

        services.AddSingleton<HttpClient>(new HttpClient() { BaseAddress = new Uri(baseAddress) });

        ConfigureServices(services);

        _serviceProvider = services.BuildServiceProvider();
    }

    protected  ICatalogItemService CatalogItemService => _serviceProvider.GetService<ICatalogItemService>();

    public object? GetService(Type serviceType) => _serviceProvider.GetService(serviceType);


    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<ICatalogItemService, CatalogItemService>();

    }
}