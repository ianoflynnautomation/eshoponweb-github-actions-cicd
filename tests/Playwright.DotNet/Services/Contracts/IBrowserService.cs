using Microsoft.Playwright;
using Playwright.DotNet.Playwright.Core;

namespace Playwright.DotNet.Services.Contracts;

public interface IBrowserService
{
    Task<string> HtmlSource { get; }
    Uri Url { get; }
    Task<string> Title { get; }
    Task<IBrowserService> GoBackAsync();
    Task<IBrowserService> GoForwardAsync();
    Task<IBrowserService> ReloadAsync();
    BrowserPage SwitchToFirstTab();
    BrowserPage SwitchToLastTab();
    IBrowserService WaitForLoadStateAsync(LoadState state = LoadState.Load);
    Task<IBrowserService> ClearSessionStorage();
    Task<IBrowserService>  ClearLocalStorage();

}



