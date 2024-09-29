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

        var exitCode = Microsoft.Playwright.Program.Main(new[] { "install" });
        if (exitCode != 0)
        {
            Console.WriteLine("Failed to install browsers");
            Environment.Exit(exitCode);
        }

        Console.WriteLine("Browsers installed");
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
