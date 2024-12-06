
using Microsoft.Playwright;
using Playwright.DotNet.Fixtures.XUnit;
using Xunit;

namespace EShopOnWeb.XUnit.InProcess.SystemTests;

public class XUnitInProcessSystemTestsBase : IAsyncLifetime
{
    protected SystemTestFixtureXUnit _fixture;

    //  private readonly ITestOutputHelper _output;

    public XUnitInProcessSystemTestsBase(SystemTestFixtureXUnit fixture)
    {
        _fixture = fixture;
    }

    protected IBrowser Browser => _fixture.BrowserTestFixture.Browser;

    protected IPage Page => _fixture.BrowserTestFixture.Page;

    public virtual async Task InitializeAsync()
    {
        await InitializeAsyncCore();
        await _fixture.BrowserTestFixture.InitializeAsync();
    }

    public virtual async Task DisposeAsync()
    {
       await _fixture.BrowserTestFixture.DisposeAsync();
         await DisposeAsyncCore();
       //_fixture.TestOutputHelper = null;
    }

    protected virtual Task InitializeAsyncCore() => Task.CompletedTask;

    protected virtual Task DisposeAsyncCore() => Task.CompletedTask;
}
