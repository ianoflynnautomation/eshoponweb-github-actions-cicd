
using Autofac.Core;
using Playwright.DotNet.DI;
using Playwright.DotNet.Infra;

namespace Playwright.DotNet.PageObjects;

/// <summary>
/// Represents a base class for web pages in the eShopOnWeb application.
/// </summary>
public abstract class WebPage
{
    protected App App => ServiceLocator.Resolve<App>();

    public abstract string Url { get; }

    public virtual void Open() => App.Navigation.Navigate(new Uri(Url));

}