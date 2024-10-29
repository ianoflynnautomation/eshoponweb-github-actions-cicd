using BlazorShared.Models;
using Playwright.DotNet.Configuration;
using Playwright.DotNet.Configuration.Options;

namespace Playwright.DotNet.Fixtures.Host;

public class ExternalTestHost : ITestHost
{
    public string? WebServerUrl => ConfigurationRootInstance.GetSection<TestHostOptions>(TestHostOptions.TestHostSection).HostUrl;


    public async Task CreateCatelogItem(CreateCatalogItemRequest catalogItem){

        await Task.CompletedTask;
    }


    public async Task DeleteCatalogItem(int catalogItemId)
    {
        await Task.CompletedTask;
    }
}