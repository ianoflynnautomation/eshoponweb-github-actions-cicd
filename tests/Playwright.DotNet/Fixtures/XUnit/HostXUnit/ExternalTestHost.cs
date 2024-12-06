using BlazorShared.Models;
using Playwright.DotNet.Configuration;
using Playwright.DotNet.Configuration.Options;

namespace Playwright.DotNet.Fixtures.XUnit.HostXUnit;

public class ExternalTestHost : ITestHostXunit
{
    public string? WebServerUrl => ConfigurationRootInstance.GetSection<TestHostOptions>(TestHostOptions.TestHostSection).HostUrl;
    public string? PublicApiUrl => ConfigurationRootInstance.GetSection<TestHostOptions>(TestHostOptions.TestHostSection).PublicApiUrl;

    public async Task CreateCatelogItem(CreateCatalogItemRequest catalogItem){

        await Task.CompletedTask;
    }


    public async Task DeleteCatalogItem(int catalogItemId)
    {
        await Task.CompletedTask;
    }

    public virtual Task InitializeAsync() => Task.CompletedTask;


    public virtual Task DisposeAsync() => Task.CompletedTask;
}