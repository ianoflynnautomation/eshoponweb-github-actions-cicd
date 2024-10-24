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
using System.Linq;
using System;
using Microsoft.AspNetCore.Identity;

namespace EShopOnWeb.TestContainersSystemTests.Fixtures;

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
        builder.ConfigureLogging(c =>
        {
            // remove the default logging providers
            c.ClearProviders();
            //c.AddSerilog();
            //     c.Services.AddSingleton<ILoggerFactory, CustomSerilogLoggerFactory>();
            //     c.Services.AddSingleton<Microsoft.Extensions.Logging.ILogger>(serviceProvider
            //    => serviceProvider.GetRequiredService<ILogger<SystemTestFixture>>());
        });

        builder.ConfigureTestServices(async services =>
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

        //    using var scope = Services.CreateScope();
        //    var scopedProvider = scope.ServiceProvider;
        //    try
        //    {
        //        var catalogContext = scopedProvider.GetRequiredService<CatalogContext>();
        //        await CatalogContextSeed.SeedAsync(catalogContext,Logger);

        //        var userManager = scopedProvider.GetRequiredService<UserManager<ApplicationUser>>();
        //        var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //        var identityContext = scopedProvider.GetRequiredService<AppIdentityDbContext>();
        //        await AppIdentityDbContextSeed.SeedAsync(identityContext, userManager, roleManager);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Logger.Error(ex, "An error occurred seeding the DB.");
        //    }


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

        builder.ConfigureHostConfiguration(config =>
          {
              Log.Logger.Information("Loading host configuration");
              config.AddJsonFile("appsettings.test.json");
          });

        builder.ConfigureServices(serviceCollection =>
        {
            serviceCollection.AddSingleton<ILoggerFactory, CustomSerilogLoggerFactory>();
            serviceCollection.AddSingleton<Microsoft.Extensions.Logging.ILogger>(serviceProvider
                => serviceProvider.GetRequiredService<ILogger<SystemTestFixture>>());
        });

        builder.UseEnvironment("Docker");

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
                _host?.Dispose();
            }

            _disposed = true;
        }
        Log.Logger.Information("ServerFixture disposed");
        Log.CloseAndFlush();
    }
}