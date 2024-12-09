
using System;
using Playwright.DotNet.Fixtures;
using Playwright.DotNet.Infra.NUnit;

namespace EShopOnWeb.NUnit.InProcess.SystemTests;

public class NUnitInProcessSystemTestsBase : PageTestBase
{
    protected SystemTestFixture _fixture;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _fixture = GetSystemTestFixture();
        
        var exitCode = Microsoft.Playwright.Program.Main(new[] { "install" });
        if (exitCode != 0)
        {
            throw new Exception($"Playwright exited with code {exitCode}");
        }

    }

    [SetUp]
    public async Task SetUp()
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
