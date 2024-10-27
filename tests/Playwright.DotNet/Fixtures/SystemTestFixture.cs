using Microsoft.Extensions.Configuration;
using Playwright.DotNet.Configuration;
using Playwright.DotNet.Configuration.Options;
using Playwright.DotNet.Fixtures.Host;
using Serilog;

namespace Playwright.DotNet.Fixtures;

public class SystemTestFixture : IDisposable
{
    private readonly IConfigurationRoot _configurationRoot;

    public SystemTestFixture()
    {
        _configurationRoot = ConfigurationRootInstance.TestConfiguration;
        SystemTestHost = GetTestHostFixture();
        Logger = CreateLogger();
    }

    public ILogger Logger { get; set; }

    public ITestHost SystemTestHost { get; }

    private ITestHost GetTestHostFixture()
    {
        var testHostOptions = new TestHostOptions();
        _configurationRoot.GetSection(TestHostOptions.TestHostSection).Bind(testHostOptions);

        return testHostOptions.HostType switch
        {
            HostType.Internal => new InternalTestHost(),
            HostType.External => new ExternalTestHost(),
            _ => throw new ArgumentOutOfRangeException(nameof(testHostOptions.HostType), testHostOptions.HostType, null)
        };
    }

    private ILogger CreateLogger()
    {
        return new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger();
    }


    public void Dispose()
    {

    }
}