using Playwright.DotNet.Fixtures;
using Playwright.DotNet.Infra.NUnit;

namespace EShopOnWeb.TestContainersSystemTests;

public class DockerContainersBaseTest : PageTestBase
{
    protected SystemTestContainersFixture _fixture;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _fixture = new SystemTestContainersFixture();

        await Task.CompletedTask;
    }

    [SetUp]
    public async Task SetUp()
    {
        await _fixture.SqlEdgeFixture.InitializeAsync();
        await Page.GotoAsync(_fixture.ServerAddress);
    }

    [TearDown]
    public async Task TearDown()
    {
        await _fixture.SqlEdgeFixture.DisposeAsync();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _fixture.Dispose();
    }
}
