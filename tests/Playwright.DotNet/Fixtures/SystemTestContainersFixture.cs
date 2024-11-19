using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.Extensions.Logging;
using Playwright.DotNet.Fixtures.DataBase;

namespace Playwright.DotNet.Fixtures;

/// <summary>
/// Represents the server fixture. 
/// </summary>
public class SystemTestContainersFixture : WebApplicationFactory<Program>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SystemTestFixture"/> class.
    /// </summary>
    public SystemTestContainersFixture()
    {
        SqlEdgeFixture = GetSqlEdgeFixture();
    }

    /// <summary>
    /// Gets the SQL Edge fixture.
    /// </summary>
    public SqlEdgeFixture SqlEdgeFixture { get; }

    private IHost? _host;
    private bool _disposed;


    private void EnsureServer()
    {
        if (_host is null)
        {
            using var _ = CreateDefaultClient();
        }
    }

    private SqlEdgeFixture GetSqlEdgeFixture()
    {
        return new SqlEdgeFixture();
    }

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
        builder.ConfigureLogging(c =>
        {
            // remove the default logging providers
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

        builder.ConfigureHostConfiguration(config =>
          {
              config.AddJsonFile("appsettings.test.json");
          });

        builder.UseEnvironment("Docker");

        builder.ConfigureWebHost(webHostBuilder => webHostBuilder.UseKestrel());

        builder.ConfigureServices(async services =>
       {
           // Add mock/test services to the builder here
           var descriptors = services.Where(d =>
                                               d.ServiceType == typeof(DbContextOptions<CatalogContext>) ||
                                               d.ServiceType == typeof(DbContextOptions<AppIdentityDbContext>))
                                           .ToList();

           foreach (var descriptor in descriptors)
           {
               services.Remove(descriptor);
           }

           // Add the catalog context and app identity context with the SQL Edge test container connection string
           services.AddDbContext<CatalogContext>(options
               => options.UseSqlServer(SqlEdgeFixture.Container.GetConnectionString()));

           services.AddDbContext<AppIdentityDbContext>(options
               => options.UseSqlServer(SqlEdgeFixture.Container.GetConnectionString()));
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