using FluentAssertions;

namespace TestContainersSystemTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class TestContainersSystemTests : PageTest
{
    public ServerFixture _fixture;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _fixture = new ServerFixture();

    }

    [SetUp]
    public async Task SetUp()
    {
        await _fixture.StartContainer();
    }

    [Test]
    public async Task Test()
    {
        await Page.GotoAsync(_fixture.ServerAddress);
        var title = await Page.TitleAsync();
        title.Should().Be("Catalog - Microsoft.eShopOnWeb");
    }

    [TearDown]
    public async Task TearDown()
    {
       await _fixture.StopContainer();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _fixture.Dispose();
    }
}
