namespace Playwright.DotNet.Services.Contracts;

public interface INavigationService
{

    Task<INavigationService> Navigate(Uri url);

    Task<INavigationService> Navigate(string url);

    Task<INavigationService> WaitForPartialUrl(string partialUrl);

}