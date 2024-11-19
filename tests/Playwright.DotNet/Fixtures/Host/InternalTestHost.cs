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


    public async Task CreateCatelogItem(CreateCatalogItemRequest catalogItem)
    {
         await Task.CompletedTask;
    }


    public async Task DeleteCatalogItem(int catalogItemId)
    {
         await Task.CompletedTask;
    }

}