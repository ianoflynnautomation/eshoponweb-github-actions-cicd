using System.Diagnostics;
using Autofac;
using Playwright.DotNet.Configuration;

namespace Playwright.DotNet.Services;

public class TestExecutionEngine
{
    public void StartBrowser(BrowserConfiguration browserConfiguration, ContainerBuilder builder)
    {
        try
        {
            var wrappedBrowser = WrappedBrowserCreateService.Create(browserConfiguration);

            builder.RegisterInstance(wrappedBrowser).AsSelf().SingleInstance();
            //builder.Register(c => (ILocator)null).As<ILocator>().InstancePerDependency();

            IsBrowserStartedCorrectly = true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            IsBrowserStartedCorrectly = false;
            throw;
        }
    }

    public bool IsBrowserStartedCorrectly { get; set; }

    public void Dispose(IContainer container)
    {
        var browser = container.Resolve<WrappedBrowser>();
        DisposeBrowserService.Dispose();
    }

    public void DisposeAll()
    {
        DisposeBrowserService.Dispose();
    }
}
