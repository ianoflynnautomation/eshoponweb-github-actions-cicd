using Microsoft.Extensions.Configuration;
using Playwright.DotNet.Configuration;
using Playwright.DotNet.Configuration.Options;
using Playwright.DotNet.Fixtures.XUnit.HostXUnit;
using Xunit;

namespace Playwright.DotNet.Fixtures.XUnit;

public class SystemTestFixtureXUnit : IAsyncLifetime
{
    private readonly IConfigurationRoot _configurationRoot;

    public SystemTestFixtureXUnit()
    {
        _configurationRoot = ConfigurationRootInstance.TestConfiguration;
        SystemTestHost = GetTestHostFixture();
    }

    public ITestHostXunit SystemTestHost { get; }

    private ITestHostXunit GetTestHostFixture()
    {
        var testHostOptions = new TestHostOptionsXUnit();
        _configurationRoot.GetSection(TestHostOptions.TestHostSection).Bind(testHostOptions);

        return testHostOptions.HostType switch
        {
            HostTypeXUnit.Internal => new InternalTestHost(),
            HostTypeXUnit.External => new ExternalTestHost(),
            HostTypeXUnit.Docker => new DockerTestFixture(),
            _ => throw new ArgumentOutOfRangeException(nameof(testHostOptions.HostType), testHostOptions.HostType, null)
        };
    }

    public async Task InitializeAsync() =>  await SystemTestHost.InitializeAsync();

    public async Task DisposeAsync() =>  await SystemTestHost.DisposeAsync();
}