using System.Text.RegularExpressions;
using Playwright.DotNet.Configuration;
using Playwright.DotNet.Configuration.Options;
using Playwright.DotNet.Services.Contracts;
using Playwright.DotNet.Playwright.Assertions;

namespace Playwright.DotNet.Services;

public class NavigationService(WrappedBrowser wrappedBrowser) : WebService(wrappedBrowser), INavigationService
{

    public async Task<INavigationService> Navigate(Uri uri)
    {
        await NavigateInternal(uri.ToString());

        return this;
    }

    public async Task<INavigationService> Navigate(string url)
    {
        try
        {
            await NavigateInternal(url);
        }
        catch (Exception)
        {
            try
            {
                await NavigateInternal(url);
            }
            catch (Exception ex)
            {
                throw new Exception($"Navigation to page {url} has failed after two attempts. Error was: {ex.Message}");
            }
        }

        return this;
    }

    public async Task<INavigationService> WaitForPartialUrl(string partialUrl)
    {
        try
        {
            await CurrentPage.Expect().NativeAssertions.ToHaveURLAsync(new Regex(@$".*{partialUrl}.*"), new() { Timeout = ConfigurationRootInstance.GetSection<WebSettingsOptions>(WebSettingsOptions.SectionName).TimeoutSettings?.InMilliseconds().WaitForPartialUrl });
        }
        catch (Exception ex)
        {
            throw new Exception($"Navigation to page with partial URL {partialUrl} has failed. Error was: {ex.Message}");
        }

        return this;
    }


    private async Task NavigateInternal(string url)
    {
        await CurrentPage.WrappedPage.GotoAsync(url);
    }
}