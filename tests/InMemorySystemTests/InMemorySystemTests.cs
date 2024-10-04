using InMemorySystemTests.Fixtures;
using FluentAssertions;

namespace InMemorySystemTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    public ServerFixture _fixture;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _fixture = new ServerFixture();
    }

    [Test]
    public async Task Test()
    {
        await Page.GotoAsync(_fixture.ServerAddress);
        var title = await Page.TitleAsync();
        title.Should().Be("Catalog - Microsoft.eShopOnWeb");
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _fixture.Dispose();
    }
}
