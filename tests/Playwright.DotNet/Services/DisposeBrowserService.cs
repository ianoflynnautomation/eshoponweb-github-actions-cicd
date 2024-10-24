using System.Diagnostics;
using Playwright.DotNet.DI;

namespace Playwright.DotNet.Services;
public static class DisposeBrowserService
{
    public static DateTime? TestRunStartTime { get; set; }

    public static void Dispose()
    {
        try
        {
            var wrappedBrowser = ServiceLocator.Resolve<WrappedBrowser>();

            wrappedBrowser.Quit();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    public static void Dispose(WrappedBrowser wrappedBrowser)
    {
        try
        {
            wrappedBrowser?.Quit();

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    public static void DisposeAll()
    {
        var wrappedBrowser = ServiceLocator.Resolve<WrappedBrowser>();

        wrappedBrowser?.Quit();

    }
}
