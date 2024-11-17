using Playwright.DotNet.Fixtures;
using Playwright.DotNet.Infra.NUnit;

namespace EShopOnWeb.TestContainersSystemTests;

public class DockerContainersBaseTest : PageTestBase
{
    protected SystemTestContainersFixture _fixture;

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
        await _fixture.SqlEdgeFixture.DisposeAsync();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _fixture.Dispose();
    }
}
