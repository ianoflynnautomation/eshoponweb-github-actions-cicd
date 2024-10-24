

using System.Diagnostics;
using System.Text.Json;
using Playwright.DotNet.Services.Contracts;

namespace Playwright.DotNet.Services;

public class JavaScriptService(WrappedBrowser wrappedBrowser) : WebService(wrappedBrowser), IJavaScriptService
{

    public JsonElement? Execute(string script, params object[] args)
    {
        try
        {
            return CurrentPage.Evaluate(script, args);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return null;
        }
    }
}
