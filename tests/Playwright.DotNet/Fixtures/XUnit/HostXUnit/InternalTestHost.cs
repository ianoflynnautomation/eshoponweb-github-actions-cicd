
namespace Playwright.DotNet.Fixtures.XUnit.HostXUnit;

public class InternalTestHost : ServerTestFixture, ITestHostXunit
{
     public string? WebServerUrl => ServerAddress;

     // TODO: Implement this
     public string? PublicApiUrl => string.Empty;

     public virtual Task InitializeAsync() => Task.CompletedTask;

     public virtual Task DisposeAsync() => Task.CompletedTask;

}