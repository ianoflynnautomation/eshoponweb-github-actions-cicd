
using Playwright.DotNet.Fixtures.XUnit;
using Playwright.DotNet.Infra.XUnit;

namespace EShopOnWeb.XUnit.InProcess.SystemTests;

public class XUnitInProcessSystemTestsBase(SystemTestFixtureXUnit fixture) : BrowserTestFixture
{
    protected SystemTestFixtureXUnit _fixture = fixture;

    public override async Task InitializeAsync()
    {
        await InitializeAsyncCore();
        await base.InitializeAsync();
    }

    public override async Task DisposeAsync()
    {
         await base.DisposeAsync();
         await DisposeAsyncCore();
    }

    protected virtual Task InitializeAsyncCore() => Task.CompletedTask;

    protected virtual Task DisposeAsyncCore() => Task.CompletedTask;
}
