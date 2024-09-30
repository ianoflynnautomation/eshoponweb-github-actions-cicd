using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Testcontainers.SqlEdge;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Identity;
using DotNet.Testcontainers.Builders;

namespace TestContainersSystemTests;

public class ServerFixture : WebApplicationFactory<Program>
{
    private const ushort MsSqlPort = 1433;
    private const string Username = "sa";
    private const string Password = "@someThingComplicated1234";
    private readonly SqlEdgeContainer _dbContainer = new SqlEdgeBuilder()
        .WithImage("mcr.microsoft.com/azure-sql-edge:1.0.7")
        .WithPortBinding(MsSqlPort, true)
        .WithEnvironment("ACCEPT_EULA", "Y")
        .WithEnvironment("SA_PASSWORD", Password)
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(MsSqlPort))
        .Build();

    public async Task StartContainer()
    {
        await _dbContainer.StartAsync();
    }

    public async Task StopContainer()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync().AsTask();
    }

    private IHost? _host;
    private bool _disposed;

    private void EnsureServer()
    {
        if (_host is null)
        {
            using var _ = CreateDefaultClient();
        }
    }

    public string ServerAddress
    {
        get
        {
            EnsureServer();
            return ClientOptions.BaseAddress.ToString();
        }
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder) =>
    builder.ConfigureTestServices(services =>
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CatalogContext>));
            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            var descriptor2 = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppIdentityDbContext>));
            if (descriptor2 is not null)
            {
                services.Remove(descriptor2);
            }

            var host = _dbContainer.Hostname;
            var port = _dbContainer.GetMappedPublicPort(MsSqlPort);

            services.AddDbContext<CatalogContext>(options =>
                options.UseSqlServer($"Server={host},{port};Database=master;User Id={Username};Password={Password};TrustServerCertificate=True"));

            services.AddDbContext<AppIdentityDbContext>(options =>
                               options.UseSqlServer($"Server={host},{port};Database=master;User Id={Username};Password={Password};TrustServerCertificate=True"));

            // using var scope = Services.CreateScope();
            // var dbContextCatalog = scope.ServiceProvider.GetRequiredService<CatalogContext>();
            // dbContextCatalog.Database.Migrate();
            // var dbContextAppIdentity = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
            // dbContextAppIdentity.Database.Migrate();

            // using var scope = Services.CreateScope();
            //        var catalogContext = scope.ServiceProvider.GetRequiredService<CatalogContext>();
            //          CatalogContextSeed.SeedAsync(catalogContext, scope.ServiceProvider.GetRequiredService<ILogger<CatalogContextSeed>>()).Wait();

            //         var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //         var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //         var identityContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
            //         AppIdentityDbContextSeed.SeedAsync(identityContext, userManager, roleManager).Wait();
        });

        builder.UseKestrel(Options =>
        {
            Options.AddServerHeader = false;
        });

        builder.UseStaticWebAssets();
        builder.UseUrls($"http://127.0.0.1:5000");
    });


    protected override IHost CreateHost(IHostBuilder builder)
    {
        var testHost = builder.Build();


        builder.UseEnvironment("Docker");

        builder.ConfigureHostConfiguration(config =>
          {
              config.AddJsonFile("appsettings.test.json");
          });

        builder.ConfigureWebHost(webHostBuilder => webHostBuilder.UseKestrel());

        _host = builder.Build();
        _host.Start();

        var server = _host.Services.GetRequiredService<IServer>();
        var addresses = server.Features.Get<IServerAddressesFeature>();

        ClientOptions.BaseAddress = addresses!.Addresses.Select(x => new Uri(x)).Last();
        testHost.Start();

        return testHost;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (!_disposed)
        {
            if (disposing)
            {
                _host?.Dispose();
            }

            _disposed = true;
        }
    }
}