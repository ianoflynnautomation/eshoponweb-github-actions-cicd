using Microsoft.Playwright;
using Playwright.DotNet.SyncPlaywright.Core;

namespace Playwright.DotNet.Services.Contracts;

public interface IBrowserService
{

    string HtmlSource { get; }
    Uri Url { get; }
    string Title { get; }
    IBrowserService Back();
    IBrowserService Forward();
    IBrowserService Refresh();
    BrowserPage SwitchToFirstTab();
    BrowserPage SwitchToLastTab();
    IBrowserService WaitForLoadState(LoadState state = LoadState.Load);
    IBrowserService ClearSessionStorage();
    IBrowserService ClearLocalStorage();
    IBrowserService WaitUntilReady();

}



