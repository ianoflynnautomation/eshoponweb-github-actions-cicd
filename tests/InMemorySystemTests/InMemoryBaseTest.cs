using Playwright.DotNet.Fixtures;
using Playwright.DotNet.Infra.NUnit;

namespace EShopOnWeb.InMemorySystemTests;

public class InMemoryBaseTest : PageTestBase
{
    protected SystemTestFixture _fixture;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _fixture = new SystemTestFixture();

        await Task.CompletedTask;
    }

    [SetUp]
    public  async Task SetUp()
    {
        await Page.GotoAsync(_fixture.SystemTestHost.WebServerUrl);

        await Task.CompletedTask;
    }

    [OneTimeTearDown]
    public  void OneTimeTearDown()
    {
        _fixture.Dispose();

    }
}
