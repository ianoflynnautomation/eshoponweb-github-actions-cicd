using System.Text.RegularExpressions;
using Playwright.DotNet.Configuration;
using Playwright.DotNet.Configuration.Options;
using Playwright.DotNet.Services.Contracts;
using Playwright.DotNet.SyncPlaywright.Assertions;

namespace Playwright.DotNet.Services;

public class NavigationService(WrappedBrowser wrappedBrowser) : WebService(wrappedBrowser), INavigationService
{

    public INavigationService Navigate(Uri uri)
    {
        NavigateInternal(uri.ToString());

        return this;
    }

    public INavigationService Navigate(string url)
    {
        try
        {
            NavigateInternal(url);
        }
        catch (Exception)
        {
            try
            {
                NavigateInternal(url);
            }
            catch (Exception ex)
            {
                throw new Exception($"Navigation to page {url} has failed after two attempts. Error was: {ex.Message}");
            }
        }

        return this;
    }

    public INavigationService WaitForPartialUrl(string partialUrl)
    {
        try
        {
            CurrentPage.Expect().ToHaveURL(new Regex(@$".*{partialUrl}.*"), new() { Timeout = ConfigurationRootInstance.GetSection<WebSettingsOptions>(WebSettingsOptions.SectionName).TimeoutSettings?.InMilliseconds().WaitForPartialUrl });
        }
        catch (Exception ex)
        {
            throw new Exception($"Navigation to page with partial URL {partialUrl} has failed. Error was: {ex.Message}");
        }

        return this;
    }


    private void NavigateInternal(string url)
    {
        _ = CurrentPage.GoTo(url);
    }
}