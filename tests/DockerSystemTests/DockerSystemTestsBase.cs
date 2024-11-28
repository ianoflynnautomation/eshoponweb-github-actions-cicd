using System;
using Playwright.DotNet.Fixtures;
using Playwright.DotNet.Infra.NUnit;

namespace EShopOnWeb.DockerSystemTests;

public class DockerSystemTestsBase : PlaywrightTestBase
{
    protected SystemTestFixture _fixture;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _fixture = GetSystemTestFixture();
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


    private SystemTestFixture GetSystemTestFixture()
    {
        return new SystemTestFixture();
    }   
}
