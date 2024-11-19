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
using System.Net.Sockets;
using System.Net;


namespace Playwright.DotNet.Fixtures;

/// <summary>
/// Represents the server fixture.
/// </summary>
public class ServerTestFixture : WebApplicationFactory<Program>
{
    private IHost? _host;
    private bool _disposed;

    public ServerTestFixture()
    {
        Client = CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

    }

    private void EnsureServer()
    {
        if (_host is null)
        {
            using var _ = CreateDefaultClient();
        }
    }

    protected HttpClient Client { get; }

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
        builder.UseUrls($"http://127.0.0.1:{GetFreeTcpPort()}");
    }

    private static int GetFreeTcpPort()
    {
        Thread.Sleep(100);
        var tcpListener = new TcpListener(IPAddress.Loopback, 0);
        tcpListener.Start();
        int port = ((IPEndPoint)tcpListener.LocalEndpoint).Port;
        tcpListener.Stop();
        return port;
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