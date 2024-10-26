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


namespace Playwright.DotNet.Fixtures;

/// <summary>
/// Represents the server fixture.
/// </summary>
public class ServerTestFixture : WebApplicationFactory<Program>
{
    private IHost? _host;
    private bool _disposed;

    private void EnsureServer()
    {
        if (_host is null)
        {
            Log.Logger.Information("Ensuring server is created");
            using var _ = CreateDefaultClient();
        }
    }

    /// <summary>
    /// Gets the server address.
    /// </summary>
    public string ServerAddress
    {
        get
        {
            EnsureServer();
            Log.Logger.Information("Server address retrieved: {ServerAddress}", ClientOptions.BaseAddress);
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
            // remove the default logging providers
            c.ClearProviders();
            c.Services.AddSingleton<ILoggerFactory, CustomSerilogLoggerFactory>();
            c.Services.AddSingleton<Microsoft.Extensions.Logging.ILogger>(serviceProvider
                => serviceProvider.GetRequiredService<ILogger<SystemTestContainersFixture>>());
        });

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
               Log.Logger.Information("Creating in-memory database for CatalogContext");
               var options = new DbContextOptionsBuilder<CatalogContext>()
               .UseInMemoryDatabase("TestCatalog")
               .UseApplicationServiceProvider(sp)
               .Options;

               return options;

           });

           services.AddScoped(sp =>
           {
               Log.Logger.Information("Creating in-memory database for CatalogContext");
               var options = new DbContextOptionsBuilder<AppIdentityDbContext>()
               .UseInMemoryDatabase("TestIdentity")
               .UseApplicationServiceProvider(sp)
               .Options;

               return options;
           });

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
        Log.Logger.Information("Creating host...");
        var testHost = builder.Build();

        builder.UseEnvironment("Development");

        builder.ConfigureHostConfiguration(config =>
          {
              Log.Logger.Information("Loading host configuration");
              config.AddJsonFile("appsettings.test.json");
          });

        builder.ConfigureWebHost(webHostBuilder => webHostBuilder.UseKestrel());

        _host = builder.Build();
        _host.Start();

        var server = _host.Services.GetRequiredService<IServer>();
        var addresses = server.Features.Get<IServerAddressesFeature>();

        ClientOptions.BaseAddress = addresses!.Addresses.Select(x => new Uri(x)).Last();
        testHost.Start();

        Log.Logger.Information("Host created and started at {BaseAddress}", ClientOptions.BaseAddress);

        return testHost;
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
                Log.Logger.Information("Disposing host");
                _host?.StopAsync();
                _host?.Dispose();
            }

            _disposed = true;
        }
    }
}