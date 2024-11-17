using System;
using Playwright.DotNet.Fixtures;
using Playwright.DotNet.Infra.NUnit;

namespace EShopOnWeb.DockerSystemTests;

public class BaseTest : PageTestBase
{
    protected SystemTestFixture _fixture;
    protected TestContext TestContext => TestContext.CurrentContext;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _fixture = new SystemTestFixture();
    }

    [SetUp]
    public async Task SetUp()
    {  

    }

    [TearDown]
    public async Task TearDown()
    {

    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _fixture.Dispose();
    }
}
