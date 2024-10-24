using EShopOnWeb.TestContainersSystemTests.Fixtures;
using FluentAssertions;

namespace EShopOnWeb.TestContainersSystemTests;

[TestFixture]
public class TestContainersSystemTests : PageTest
{
    public SystemTestFixture _fixture;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _fixture = new SystemTestFixture();

    }

    [SetUp]
    public async Task SetUp()
    {
        await _fixture.SqlEdgeFixture.InitializeAsync();
    }

    [Test]
    public async Task Test()
    {
        await Page.GotoAsync(_fixture.ServerAddress);
        var title = await Page.TitleAsync();
        title.Should().Be("Catalog - Microsoft.eShopOnWeb");
    }

    [Test]
    public async Task Test2()
    {
        await Page.GotoAsync(_fixture.ServerAddress);
        var title = await Page.TitleAsync();
        title.Should().Be("Catalog - Microsoft.eShopOnWeb");
    }

    [TearDown]
    public async Task TearDown()
    {
        await _fixture.SqlEdgeFixture.StopContainer();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _fixture.SqlEdgeFixture.DisposeAsync();
        _fixture.Dispose();
    }
}
