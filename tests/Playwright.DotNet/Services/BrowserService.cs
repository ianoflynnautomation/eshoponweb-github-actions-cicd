

using System.Diagnostics;
using Microsoft.Playwright;
using Playwright.DotNet.Services.Contracts;
using Playwright.DotNet.SyncPlaywright.Core;

namespace Playwright.DotNet.Services;

/// <summary>
/// Service for interacting with the browser.
/// </summary>
/// <param name="wrappedBrowser"></param>
public class BrowserService(WrappedBrowser wrappedBrowser) : WebService(wrappedBrowser), IBrowserService
{
    public string HtmlSource => CurrentPage.Content();

    public Uri Url => new(CurrentPage.Url);

    public string Title => CurrentPage.Title();

    public IBrowserService Back()
    {
        CurrentPage.GoBack();

        return this;
    }

    public IBrowserService Forward()
    {
        CurrentPage.GoForward();

        return this;
    }

    public IBrowserService Refresh()
    {
        CurrentPage.Reload();

        return this;
    }

    public BrowserPage SwitchToFirstTab() => WrappedBrowser.CurrentContext.Pages.First();

    public BrowserPage SwitchToLastTab() => WrappedBrowser.CurrentContext.Pages.Last();

    
    // Faster than sending js and checking for a X state.
    public IBrowserService WaitForLoadState(LoadState state = LoadState.Load)
    {
        WrappedBrowser.CurrentPage.WaitForLoadState(state);

        return this;
    }

    public IBrowserService ClearSessionStorage()
    {
        var javaScriptService = new JavaScriptService(WrappedBrowser);
        javaScriptService.Execute("sessionStorage.clear();");

        return this;
    }

    public IBrowserService ClearLocalStorage()
    {
        var javaScriptService = new JavaScriptService(WrappedBrowser);
        javaScriptService.Execute("localStorage.clear();");

        return this;
    }


    private string? InvokeScript(string scriptToInvoke)
    {
        JavaScriptService javaScriptService = new JavaScriptService(WrappedBrowser);
        return javaScriptService.Execute(scriptToInvoke)?.ToString();
    }

    public IBrowserService WaitUntilReady()
    {
        int maxSeconds = 60;

        Utilities.Wait.ForConditionUntilTimeout(
                () =>
                {
                    try
                    {
                        var isReady = InvokeScript("document.readyState") == "complete";

                        if (isReady)
                        {
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Exception occurred while waiting for ReadyState = complete. Message: {ex.Message}");
                    }

                    return false;
                },
                maxSeconds,
                sleepTimeMilliseconds: 100);

        return this;
    }
}
