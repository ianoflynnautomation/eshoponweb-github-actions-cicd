using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BlazorShared.Models;
using Playwright.DotNet.Configuration;
using Playwright.DotNet.Configuration.Options;

namespace Playwright.DotNet.Fixtures.Host;

public class DockerTestFixture : ITestHost, IDisposable
{

    protected HttpClient Client { get; }

    public DockerTestFixture()
    {
        Client = new HttpClient();
        
    }
    public string? WebServerUrl => ConfigurationRootInstance.GetSection<TestHostOptions>(TestHostOptions.TestHostSection).HostUrl;

    public string? PublicApiUrl => ConfigurationRootInstance.GetSection<TestHostOptions>(TestHostOptions.TestHostSection).PublicApiUrl;

    public async Task CreateCatelogItem(CreateCatalogItemRequest catalogItem)
    {
        var adminToken = ApiTokenHelper.GetAdminUserToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
        var json = JsonSerializer.Serialize(catalogItem);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await Client.PostAsync($"{PublicApiUrl}catalog-items", data);
        response.EnsureSuccessStatusCode();
        
    }

    public async Task CleanDatabase()
    {
         await Task.CompletedTask;
    }

    public Task DeleteCatalogItem(int catalogItemId)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        Client?.Dispose();
    }
}