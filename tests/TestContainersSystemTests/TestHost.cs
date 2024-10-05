﻿
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TestContainersSystemTests;

public sealed class TestHost : ITestHost, IDisposable
{
    protected readonly IHost host = Host
     .CreateDefaultBuilder()
     .ConfigureServices(serviceCollection =>
     {
         //serviceCollection.AddSingleton<ILoggerFactory, CustomSerilogLoggerFactory>();
         //serviceCollection.AddSingleton<ILogger>(serviceProvider => serviceProvider.GetRequiredService<ILogger<TestHost>>());
         //serviceCollection.AddHostedService<Initialization>();
     })
     .Build();

    /// <inheritdoc />
    public Task InitializeAsync()
    {
        return host.StartAsync();
    }

    /// <inheritdoc />
    public Task DisposeAsync()
    {
        return host.StopAsync();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        host.Dispose();
    }
}
