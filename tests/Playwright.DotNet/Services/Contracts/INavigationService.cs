namespace Playwright.DotNet.Services.Contracts;

public interface INavigationService
{

    INavigationService Navigate(Uri url);

    INavigationService Navigate(string url);

    INavigationService WaitForPartialUrl(string partialUrl);

}