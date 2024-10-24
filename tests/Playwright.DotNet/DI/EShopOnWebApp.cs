using Playwright.DotNet.PageObjects.Basket;
using Playwright.DotNet.PageObjects.Basket.CheckoutPage;
using Playwright.DotNet.PageObjects.Basket.SuccessPage;
using Playwright.DotNet.PageObjects.HomePage;
using Playwright.DotNet.PageObjects.Sections;
using Playwright.Dotnet.PageObjects.LoginPage;
using Autofac;

namespace Playwright.DotNet.DI;

public class EShopOnWebApp(IContainer container)
{
    private IContainer _container = container;

    public IHeaderSection HeaderSection => _container.Resolve<IHeaderSection>();
    public ILoginPage LoginPage => _container.Resolve<ILoginPage>();
    public IHomePage HomePage => _container.Resolve<IHomePage>();
    public IBasketPage BasketPage => _container.Resolve<IBasketPage>();
    public ICheckoutPage CheckoutPage => _container.Resolve<ICheckoutPage>();
    public ISuccessPage SuccessPage => _container.Resolve<ISuccessPage>();

}