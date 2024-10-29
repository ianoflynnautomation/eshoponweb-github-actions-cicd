using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BlazorAdmin.Services;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.DependencyInjection;


namespace Playwright.DotNet.Fixtures.Host;

public class InternalTestHost : ServerTestFixture,  ITestHost
{
    public string? WebServerUrl => ServerAddress;


    //protected CatalogItemService CatalogItemService => ServiceProvider.GetRequiredService<CatalogItemService>();


    public async Task CreateCatelogItem(CreateCatalogItemRequest catalogItem)
    {
        var adminToken = ApiTokenHelper.GetAdminUserToken();
        var client = ProgramTest.NewClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
        var json = JsonSerializer.Serialize(catalogItem);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("api/catalog-items", data);
        response.EnsureSuccessStatusCode();
    }


    public async Task DeleteCatalogItem(int catalogItemId)
    {
        var adminToken = ApiTokenHelper.GetAdminUserToken();
        var client = ProgramTest.NewClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
        var response = await client.DeleteAsync($"api/catalog-items/{catalogItemId}");
        response.EnsureSuccessStatusCode();
    }

}