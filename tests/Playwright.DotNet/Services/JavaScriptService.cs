using System.Diagnostics;
using System.Text.Json;
using Playwright.DotNet.Services.Contracts;

namespace Playwright.DotNet.Services;

public class JavaScriptService(WrappedBrowser wrappedBrowser) : WebService(wrappedBrowser), IJavaScriptService
{
    public async Task<JsonElement?> ExecuteAsync(string script, params object[] args)
    {
        try
        {
            return await CurrentPage.WrappedPage.EvaluateAsync(script, args);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return null;
        }
    }
}
