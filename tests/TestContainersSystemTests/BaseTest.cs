
using Playwright.DotNet.Fixtures;

namespace EShopOnWeb.TestContainersSystemTests;

public class BaseTest : PageTest
{
    protected SystemTestContainersFixture _fixture;

    protected TestContext TestContext => TestContext.CurrentContext;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _fixture = new SystemTestContainersFixture();
    }

    [SetUp]
    public async Task SetUp()
    {
        await _fixture.SqlEdgeFixture.InitializeAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        //await _fixture.SqlEdgeFixture.StopContainer();
        await _fixture.SqlEdgeFixture.DisposeAsync();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _fixture.Dispose();
    }
}
