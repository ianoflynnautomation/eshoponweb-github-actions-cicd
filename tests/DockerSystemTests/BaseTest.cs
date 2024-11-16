using Playwright.DotNet.Fixtures;

namespace EShopOnWeb.DockerSystemTests;

public class BaseTest : PageTest
{
    protected SystemTestFixture _fixture;
    protected TestContext TestContext => TestContext.CurrentContext;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _fixture = new SystemTestFixture();
    }

    [SetUp]
    public void SetUp()
    {  

    }

    [TearDown]
    public void TearDown()
    {

    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _fixture.Dispose();
    }
}
