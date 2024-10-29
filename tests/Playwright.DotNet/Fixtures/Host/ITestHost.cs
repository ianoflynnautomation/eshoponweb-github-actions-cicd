
using BlazorShared.Models;

namespace Playwright.DotNet.Fixtures.Host;

public interface ITestHost
{
    public string? WebServerUrl { get; }

    Task CreateCatelogItem(CreateCatalogItemRequest catalogItem);


    Task DeleteCatalogItem(int catalogItemId);
}