using BlazorShared.Models;


namespace Playwright.DotNet.Fixtures.Host;

public class InternalTestHost : ServerTestFixture,  ITestHost
{
    public string? WebServerUrl => ServerAddress;

    // TODO: Implement this
    public string? PublicApiUrl => string.Empty;


    public async Task CreateCatelogItem(CreateCatalogItemRequest catalogItem)
    {
         await Task.CompletedTask;
    }


    public async Task DeleteCatalogItem(int catalogItemId)
    {
         await Task.CompletedTask;
    }

}