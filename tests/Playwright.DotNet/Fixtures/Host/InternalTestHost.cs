using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BlazorShared.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;


namespace Playwright.DotNet.Fixtures.Host;

public class InternalTestHost : ServerTestFixture,  ITestHost
{
    public string? WebServerUrl => ServerAddress;

    // TODO: Implement this
    public string? PublicApiUrl => string.Empty;


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
        //CatalogContext.Database.ExecuteSqlRaw("DELETE FROM Catalog WHERE Id = {0}", catalogItemId);
    }

}