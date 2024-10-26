using Playwright.DotNet.Configuration;
using Playwright.DotNet.Configuration.Options;

namespace Playwright.DotNet.Fixtures.Host;

public class ExternalTestHost : ITestHost
{
    public string? WebServerUrl => ConfigurationRootInstance.GetSection<TestHostOptions>(TestHostOptions.TestHostSection).HostUrl;
}