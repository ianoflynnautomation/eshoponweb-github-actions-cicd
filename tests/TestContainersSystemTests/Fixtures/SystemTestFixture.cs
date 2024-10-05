using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.Extensions.Logging;
using Serilog;

namespace TestContainersSystemTests.Fixtures;

/// <summary>
/// Represents the server fixture. 
/// </summary>
public class SystemTestFixture : WebApplicationFactory<Program>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SystemTestFixture"/> class.
    /// </summary>
    public SystemTestFixture()
    {
        SqlEdgeFixture = new SqlEdgeFixture();
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
            Log.Information("Ensuring server is created");
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
            Log.Information("Server address retrieved: {ServerAddress}", ClientOptions.BaseAddress);
            return ClientOptions.BaseAddress.ToString();
        }
    }

    /// <summary>
    /// Configures the web host.
    /// </summary>
    protected override void ConfigureWebHost(IWebHostBuilder builder) =>
    builder.ConfigureTestServices(services =>
    {
        Log.Information("Configuring WebHost");
        //services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
        services.AddSingleton<ILoggerFactory, CustomSerilogLoggerFactory>();
        services.AddSingleton<Microsoft.Extensions.Logging.ILogger>(serviceProvider
        => serviceProvider.GetRequiredService<ILogger<SystemTestFixture>>());

        base.ConfigureWebHost(builder);

        builder.ConfigureServices(services =>
        {
            Log.Information("Configuring services");
            // Remove the existing catalog context and app identity context
            var catalogContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<CatalogContext>));

            if (catalogContextDescriptor is not null)
            {
                 Log.Information("Removing existing CatalogContext DbContextOptions");
                services.Remove(catalogContextDescriptor);
            }

            var catalogContextDescriptorDscriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<CatalogContext>));

                Log.Information("Removing existing AppIdentityDbContext DbContextOptions");
            if (catalogContextDescriptorDscriptor is not null)
            {
                services.Remove(catalogContextDescriptorDscriptor);
            }

            // Add the catalog context and app identity context with the SQL Edge test container connection string
            services.AddDbContext<CatalogContext>(options
                => options.UseSqlServer(SqlEdgeFixture.Container.GetConnectionString()));

            services.AddDbContext<AppIdentityDbContext>(options
                => options.UseSqlServer(SqlEdgeFixture.Container.GetConnectionString()));
        });

        builder.UseKestrel(Options =>
        {
            Options.AddServerHeader = false;
        });

        builder.UseStaticWebAssets();
        builder.UseUrls($"http://127.0.0.1:5000");
    });


    /// <summary>
    /// Creates the host.
    /// </summary>
    protected override IHost CreateHost(IHostBuilder builder)
    {
        Log.Information("Creating host...");
        var testHost = builder.Build();

        builder.UseEnvironment("Docker");

        builder.ConfigureHostConfiguration(config =>
          {
             Log.Information("Loading host configuration");
              config.AddJsonFile("appsettings.test.json");
          });

        builder.ConfigureWebHost(webHostBuilder => webHostBuilder.UseKestrel());

        _host = builder.Build();
        _host.Start();

        var server = _host.Services.GetRequiredService<IServer>();
        var addresses = server.Features.Get<IServerAddressesFeature>();

        ClientOptions.BaseAddress = addresses!.Addresses.Select(x => new Uri(x)).Last();
        testHost.Start();

        Log.Information("Host created and started at {BaseAddress}", ClientOptions.BaseAddress);

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
                Log.Information("Disposing host");
                _host?.Dispose();
            }

            _disposed = true;
        }
        Log.Information("ServerFixture disposed");
    }
}