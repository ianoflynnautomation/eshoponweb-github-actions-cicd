
using BlazorShared.Models;

namespace Playwright.DotNet.Fixtures.Host;

public interface ITestHost
{
    public string? WebServerUrl { get; }

    public string? PublicApiUrl { get; }

    Task CreateCatelogItem(CreateCatalogItemRequest catalogItem);


    Task DeleteCatalogItem(int catalogItemId);
}