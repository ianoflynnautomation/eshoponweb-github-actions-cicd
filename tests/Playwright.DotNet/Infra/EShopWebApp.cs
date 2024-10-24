
using Playwright.DotNet.DI;
using Playwright.DotNet.Services;
using Playwright.DotNet.Services.Contracts;

namespace Playwright.DotNet.Infra;

/// <summary>
/// Represents the eShopOnWeb application.
/// </summary>
public class EShopWebApp : IDisposable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EShopWebApp"/> class.
    /// </summary>
    public EShopWebApp()
    {
    }

    /// <summary>
    /// Gets the browser service.
    /// </summary>
    public IBrowserService Browser => ServiceLocator.Resolve<IBrowserService>();

    /// <summary>
    /// Gets the component create service.
    /// </summary>
    public IComponentCreateService Component => ServiceLocator.Resolve<IComponentCreateService>();

    /// <summary>
    /// Gets the navigation service.
    /// </summary>
    public INavigationService Navigation => ServiceLocator.Resolve<INavigationService>();

    /// <summary>
    /// Disposes the eShopOnWeb application.
    /// </summary>
    public void Dispose()
    {
        DisposeBrowserService.Dispose();
        GC.SuppressFinalize(this);
    }
}