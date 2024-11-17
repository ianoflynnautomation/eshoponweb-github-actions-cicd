using Playwright.DotNet.Fixtures;
using Playwright.DotNet.Infra.NUnit;

namespace EShopOnWeb.InMemorySystemTests;

public class InMemoryBaseTest : PageTestBase
{
    protected SystemTestFixture _fixture;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _fixture = new SystemTestFixture();

    }

    [SetUp]
    public async Task SetUp()
    {
        await Page.GotoAsync(_fixture.SystemTestHost.WebServerUrl);

    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _fixture.Dispose();

    }
}
