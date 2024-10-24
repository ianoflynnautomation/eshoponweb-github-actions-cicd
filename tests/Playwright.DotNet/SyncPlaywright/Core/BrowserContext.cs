﻿using Microsoft.Playwright;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Services.WebApi;

namespace Playwright.DotNet.SyncPlaywright.Core;

/// <summary>
/// Synchronous wrapper of Playwright IBrowserContext.
/// </summary>
public partial class BrowserContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BrowserContext"/> class.
    /// </summary>
    internal BrowserContext(PlaywrightBrowser browser, IBrowserContext context)
    {
        Browser = browser;
        WrappedBrowserContext = context;
    }

    internal BrowserContext(IBrowserContext context)
    {
        WrappedBrowserContext = context;
    }

    public IBrowserContext WrappedBrowserContext { get; internal init; }

    public PlaywrightBrowser Browser { get; internal init; }

    internal List<BrowserPage> BrowserPages { get; private init; } = new List<BrowserPage>();

    public IReadOnlyList<BrowserPage> Pages => BrowserPages.AsReadOnly();

    public IAPIRequestContext APIRequest => WrappedBrowserContext.APIRequest;

    public ITracing Tracing => WrappedBrowserContext.Tracing;

    public void AddCookies(IEnumerable<Cookie> cookies)
    {
        WrappedBrowserContext.AddCookiesAsync(cookies).SyncResult();
    }

    public void AddInitScript(string script = null, string scriptPath = null)
    {
        WrappedBrowserContext.AddInitScriptAsync(script, scriptPath).SyncResult();
    }

    public void ClearCookies()
    {
        WrappedBrowserContext.ClearCookiesAsync().SyncResult();
    }

    public void ClearPermissions()
    {
        WrappedBrowserContext.ClearPermissionsAsync().SyncResult();
    }

    public void Close(BrowserContextCloseOptions options = null)
    {
        WrappedBrowserContext.CloseAsync(options).SyncResult();
    }

    public IReadOnlyList<BrowserContextCookiesResult> Cookies(IEnumerable<string> urls = null)
    {
        return WrappedBrowserContext.CookiesAsync(urls).SyncResult();
    }

    public void Dispose()
    {
        WrappedBrowserContext.DisposeAsync().GetAwaiter().GetResult();
    }

    public void ExposeBinding(string name, Action callback, BrowserContextExposeBindingOptions options = null)
    {
        WrappedBrowserContext.ExposeBindingAsync(name, callback, options).SyncResult();
    }

    public void ExposeBinding(string name, Action<BindingSource> callback)
    {
        WrappedBrowserContext.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<T>(string name, Action<BindingSource, T> callback)
    {
        WrappedBrowserContext.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<TResult>(string name, Func<BindingSource, TResult> callback)
    {
        WrappedBrowserContext.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<TResult>(string name, Func<BindingSource, IJSHandle, TResult> callback)
    {
        WrappedBrowserContext.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<T, TResult>(string name, Func<BindingSource, T, TResult> callback)
    {
        WrappedBrowserContext.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<T1, T2, TResult>(string name, Func<BindingSource, T1, T2, TResult> callback)
    {
        WrappedBrowserContext.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<T1, T2, T3, TResult>(string name, Func<BindingSource, T1, T2, T3, TResult> callback)
    {
        WrappedBrowserContext.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<T1, T2, T3, T4, TResult>(string name, Func<BindingSource, T1, T2, T3, T4, TResult> callback)
    {
        WrappedBrowserContext.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeFunction(string name, Action callback)
    {
        WrappedBrowserContext.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void ExposeFunction<T>(string name, Action<T> callback)
    {
        WrappedBrowserContext.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void ExposeFunction<TResult>(string name, Func<TResult> callback)
    {
        WrappedBrowserContext.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void ExposeFunction<T, TResult>(string name, Func<T, TResult> callback)
    {
        WrappedBrowserContext.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void ExposeFunction<T1, T2, TResult>(string name, Func<T1, T2, TResult> callback)
    {
        WrappedBrowserContext.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void ExposeFunction<T1, T2, T3, TResult>(string name, Func<T1, T2, T3, TResult> callback)
    {
        WrappedBrowserContext.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void ExposeFunction<T1, T2, T3, T4, TResult>(string name, Func<T1, T2, T3, T4, TResult> callback)
    {
        WrappedBrowserContext.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void GrantPermissions(IEnumerable<string> permissions, BrowserContextGrantPermissionsOptions options = null)
    {
        WrappedBrowserContext.GrantPermissionsAsync(permissions, options).SyncResult();
    }

    public ICDPSession NewCDPSession(BrowserPage page)
    {
        return WrappedBrowserContext.NewCDPSessionAsync(page.WrappedPage).SyncResult();
    }

    public ICDPSession NewCDPSession(IFrame page)
    {
        return WrappedBrowserContext.NewCDPSessionAsync(page).SyncResult();
    }

    public BrowserPage NewPage()
    {
        var newPage = new BrowserPage(this, WrappedBrowserContext.NewPageAsync().SyncResult());
        BrowserPages.Add(newPage);

        return newPage;
    }

    public void Route(string url, Action<IRoute> handler, BrowserContextRouteOptions options = null)
    {
        WrappedBrowserContext.RouteAsync(url, handler, options).SyncResult();
    }

    public void Route(Regex url, Action<IRoute> handler, BrowserContextRouteOptions options = null)
    {
        WrappedBrowserContext.RouteAsync(url, handler, options).SyncResult();
    }

    public void Route(Func<string, bool> url, Action<IRoute> handler, BrowserContextRouteOptions options = null)
    {
        WrappedBrowserContext.RouteAsync(url, handler, options).SyncResult();
    }

    public void Route(string url, Func<IRoute, Task> handler, BrowserContextRouteOptions options = null)
    {
        WrappedBrowserContext.RouteAsync(url, handler, options).SyncResult();
    }

    public void Route(Regex url, Func<IRoute, Task> handler, BrowserContextRouteOptions options = null)
    {
        WrappedBrowserContext.RouteAsync(url, handler, options).SyncResult();
    }

    public void Route(Func<string, bool> url, Func<IRoute, Task> handler, BrowserContextRouteOptions options = null)
    {
        WrappedBrowserContext.RouteAsync(url, handler, options).SyncResult();
    }

    public void RouteFromHAR(string har, BrowserContextRouteFromHAROptions options = null)
    {
        WrappedBrowserContext.RouteFromHARAsync(har, options).SyncResult();
    }

    public IConsoleMessage RunAndWaitForConsoleMessage(Func<Task> action, BrowserContextRunAndWaitForConsoleMessageOptions options = null)
    {
        return WrappedBrowserContext.RunAndWaitForConsoleMessageAsync(action, options).SyncResult();
    }

    public BrowserPage RunAndWaitForPage(Func<Task> action, BrowserContextRunAndWaitForPageOptions options = null)
    {
        var page = new BrowserPage(this, WrappedBrowserContext.RunAndWaitForPageAsync(action, options).SyncResult());
        BrowserPages.Add(page);

        return page;
    }

    public void SetDefaultNavigationTimeout(float timeout)
    {
        WrappedBrowserContext.SetDefaultNavigationTimeout(timeout);
    }

    public void SetDefaultTimeout(float timeout)
    {
        WrappedBrowserContext.SetDefaultTimeout(timeout);
    }

    public void SetExtraHTTPHeaders(IEnumerable<KeyValuePair<string, string>> headers)
    {
        WrappedBrowserContext.SetExtraHTTPHeadersAsync(headers).SyncResult();
    }

    public void SetGeolocation(Geolocation geolocation)
    {
        WrappedBrowserContext.SetGeolocationAsync(geolocation).SyncResult();
    }

    public void SetOffline(bool offline)
    {
        WrappedBrowserContext.SetOfflineAsync(offline).SyncResult();
    }

    public string StorageState(BrowserContextStorageStateOptions options = null)
    {
        return WrappedBrowserContext.StorageStateAsync(options).SyncResult();
    }

    public void Unroute(string url, Action<IRoute> handler = null)
    {
        WrappedBrowserContext.UnrouteAsync(url, handler).SyncResult();
    }

    public void Unroute(Regex url, Action<IRoute> handler = null)
    {
        WrappedBrowserContext.UnrouteAsync(url, handler).SyncResult();
    }

    public void Unroute(Func<string, bool> url, Action<IRoute> handler = null)
    {
        WrappedBrowserContext.UnrouteAsync(url, handler).SyncResult();
    }

    public void Unroute(string url, Func<IRoute, Task> handler)
    {
        WrappedBrowserContext.UnrouteAsync(url, handler).SyncResult();
    }

    public void Unroute(Regex url, Func<IRoute, Task> handler)
    {
        WrappedBrowserContext.UnrouteAsync(url, handler).SyncResult();
    }

    public void Unroute(Func<string, bool> url, Func<IRoute, Task> handler)
    {
        WrappedBrowserContext.UnrouteAsync(url, handler).SyncResult();
    }

    public IConsoleMessage WaitForConsoleMessage(BrowserContextWaitForConsoleMessageOptions options = null)
    {
        return WrappedBrowserContext.WaitForConsoleMessageAsync(options).SyncResult();
    }

    public BrowserPage WaitForPage(BrowserContextWaitForPageOptions options = null)
    {
        var page = new BrowserPage(this, WrappedBrowserContext.WaitForPageAsync(options).SyncResult());
        BrowserPages.Add(page);

        return page;
    }
}
