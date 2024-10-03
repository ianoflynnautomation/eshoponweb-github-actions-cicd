
using DotNet.Testcontainers.Builders;
using NuGet.Protocol.Plugins;
using Testcontainers.SqlEdge;

namespace TestContainersSystemTests.Fixtures;

public sealed class SqlEdgeFixture : DataBaseFixture<SqlEdgeContainer>
{
    public SqlEdgeFixture()
    {
        Container = new SqlEdgeBuilder()
        .WithImage("mcr.microsoft.com/azure-sql-edge:latest")
        .WithPortBinding(1433, true)
        .WithEnvironment("ACCEPT_EULA", "Y")
        .WithEnvironment("SA_PASSWORD", "yourStrong(!)Password")
        //.WithEnvironment("MSSQL_SA_PASSWORD", "yourStrong(!)Password")
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
        //.WithWaitStrategy(Wait.ForUnixContainer().UntilContainerIsHealthy())
        .Build();
    }

    /// <inheritdoc />
    public override async Task DisposeAsync()
    {
        await Container.DisposeAsync().ConfigureAwait(false); ;
    }

    /// <inheritdoc />
    public override async Task InitializeAsync()
    {
        await Container.StartAsync().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public override async Task StopContainer()
    {
        await Container.StopAsync().ConfigureAwait(false);
    }
}
