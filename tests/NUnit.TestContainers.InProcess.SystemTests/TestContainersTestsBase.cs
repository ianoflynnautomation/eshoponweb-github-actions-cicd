using Playwright.DotNet.Fixtures;
using Playwright.DotNet.Infra.NUnit;

namespace EShopOnWeb.NUnit.TestContainers.InProcess.SystemTests;

public class TestContainersTestsBase : PageTestBase
{
    protected SystemTestContainersFixture _fixture;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _fixture = GetSystemTestContainersFixture();

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

    private SystemTestContainersFixture GetSystemTestContainersFixture()
    {
        return new SystemTestContainersFixture();
    }
}
