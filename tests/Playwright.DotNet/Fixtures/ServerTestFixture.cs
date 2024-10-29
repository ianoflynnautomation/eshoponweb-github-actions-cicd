using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.Extensions.Logging;
using Playwright.DotNet.Logging;
using Serilog;
using BlazorAdmin.Pages.CatalogItemPage;
using BlazorAdmin.Services;
using BlazorShared.Interfaces;
using Docker.DotNet.Models;


namespace Playwright.DotNet.Fixtures;

/// <summary>
/// Represents the server fixture.
/// </summary>
public class ServerTestFixture : WebApplicationFactory<Program>
{
    private IHost? _host;
    private bool _disposed;
    private IServiceProvider _serviceProvider;

    public ServerTestFixture()
    {
        Client = CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        ServiceProvider = new ServiceCollection().BuildServiceProvider();

    }

    private void EnsureServer()
    {
        if (_host is null)
        {
            using var _ = CreateDefaultClient();
        }
    }

    protected HttpClient Client { get; }

    protected IServiceProvider ServiceProvider { get; private set; }

    /// <summary>
    /// Gets the server address.
    /// </summary>
    public string ServerAddress
    {
        get
        {
            EnsureServer();
            return ClientOptions.BaseAddress.ToString();
        }
    }

    /// <summary>
    /// Configures the web host.
    /// </summary>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureLogging(c =>
        {
            c.ClearProviders();
        });


        builder.UseKestrel(Options =>
        {
            Options.AddServerHeader = false;
        });

        builder.UseStaticWebAssets();
        builder.UseUrls($"http://127.0.0.1:5000");
    }

    /// <summary>
    /// Creates the host.
    /// </summary>
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var testHost = builder.Build();

        builder.UseEnvironment("Development");

        builder.ConfigureHostConfiguration(config =>
          {
              config.AddJsonFile("appsettings.test.json");
          });

        builder.ConfigureWebHost(webHostBuilder => webHostBuilder.UseKestrel());

        builder.ConfigureServices(services =>
       {
           var descriptors = services.Where(d =>
                                       d.ServiceType == typeof(DbContextOptions<CatalogContext>) ||
                                       d.ServiceType == typeof(DbContextOptions<AppIdentityDbContext>))
                                   .ToList();

           foreach (var descriptor in descriptors)
           {
               services.Remove(descriptor);
           }

           services.AddScoped(sp =>
           {
               var options = new DbContextOptionsBuilder<CatalogContext>()
               .UseInMemoryDatabase("TestCatalog")
               .UseApplicationServiceProvider(sp)
               .Options;

               return options;

           });

           services.AddScoped(sp =>
           {
               var options = new DbContextOptionsBuilder<AppIdentityDbContext>()
               .UseInMemoryDatabase("TestIdentity")
               .UseApplicationServiceProvider(sp)
               .Options;

               return options;
           });

       });

        _host = builder.Build();
        _host.Start();

        var server = _host.Services.GetRequiredService<IServer>();
        var addresses = server.Features.Get<IServerAddressesFeature>();

        ClientOptions.BaseAddress = addresses!.Addresses.Select(x => new Uri(x)).Last();
        testHost.Start();

        return testHost;
    }


    protected T GetWebServerService<T>()
    {
        if (_host is null)
        {
            return ServiceProvider.GetRequiredService<T>();
        }
        else
        {

            return _host.Services.GetRequiredService<T>();
        }
    }

    protected T GetScopedService<T>()
    {
        using var scope = _host.Services.CreateScope();
        return scope.ServiceProvider.GetRequiredService<T>();
    }

    /// <summary>
    /// Disposes the host.
    /// </summary>
    /// <param name="disposing"></param>
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (!_disposed)
        {
            if (disposing)
            {
                _host?.StopAsync();
                _host?.Dispose();
            }

            _disposed = true;
        }
    }
}