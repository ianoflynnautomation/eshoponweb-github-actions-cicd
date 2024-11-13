

using System.Diagnostics;
using Microsoft.Playwright;
using Playwright.DotNet.Services.Contracts;
using Playwright.DotNet.Playwright.Core;

namespace Playwright.DotNet.Services;

/// <summary>
/// Service for interacting with the browser.
/// </summary>
/// <param name="wrappedBrowser"></param>
public class BrowserService(WrappedBrowser wrappedBrowser) : WebService(wrappedBrowser), IBrowserService
{
    public Task<string> HtmlSource => CurrentPage.WrappedPage.ContentAsync();

    public Uri Url => new(CurrentPage.Url);

    public Task<string> Title => CurrentPage.WrappedPage.TitleAsync();

    public async Task<IBrowserService> GoBackAsync()
    {
        await CurrentPage.WrappedPage.GoBackAsync();
        
        return this;
    }

    public async Task<IBrowserService> GoForwardAsync()
    {
       await CurrentPage.WrappedPage.GoForwardAsync();

        return this;
    }

    public async Task<IBrowserService> ReloadAsync()
    {
        await CurrentPage.WrappedPage.ReloadAsync();

        return this;
    }

    public BrowserPage SwitchToFirstTab() => WrappedBrowser.CurrentContext.Pages.First();

    public BrowserPage SwitchToLastTab() => WrappedBrowser.CurrentContext.Pages.Last();

    
    // Faster than sending js and checking for a X state.
    public IBrowserService WaitForLoadStateAsync(LoadState state = LoadState.Load)
    {
        CurrentPage.WrappedPage.WaitForLoadStateAsync(state);
        return this;
    }

    public async Task<IBrowserService> ClearSessionStorage()
    {
        var javaScriptService = new JavaScriptService(WrappedBrowser);
       await javaScriptService.ExecuteAsync("sessionStorage.clear();");

        return this;
    }

    public async Task<IBrowserService> ClearLocalStorage()
    {
        var javaScriptService = new JavaScriptService(WrappedBrowser);
        await javaScriptService.ExecuteAsync("localStorage.clear();");

        return this;
    }


    private string? InvokeScript(string scriptToInvoke)
    {
        JavaScriptService javaScriptService = new JavaScriptService(WrappedBrowser);
        return javaScriptService.ExecuteAsync(scriptToInvoke)?.ToString();
    }
}
