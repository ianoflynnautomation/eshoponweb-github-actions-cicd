using Xunit;

namespace Playwright.DotNet.Fixtures.XUnit.HostXUnit;

public interface ITestHostXunit : IAsyncLifetime
{
    public string? WebServerUrl { get; }

    public string? PublicApiUrl { get; }

}