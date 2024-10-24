using Xunit;

namespace EShopOnWeb.FunctionalTests.Web.Pages;

[Collection("Sequential")]
public class HomePageOnGet(TestApplication factory) : IClassFixture<TestApplication>
{
    public HttpClient Client { get; } = factory.CreateClient();

    [Fact]
    public async Task ReturnsHomePageWithProductListing()
    {
        // Arrange & Act
        var response = await Client.GetAsync("/");
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Contains(".NET Bot Black Sweatshirt", stringResponse);
    }
}
