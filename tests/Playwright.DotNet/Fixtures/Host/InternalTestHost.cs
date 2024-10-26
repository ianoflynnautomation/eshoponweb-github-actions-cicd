namespace Playwright.DotNet.Fixtures.Host;

public class InternalTestHost : ServerTestFixture,  ITestHost
{
    public string? WebServerUrl => ServerAddress;
}