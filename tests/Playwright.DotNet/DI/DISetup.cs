using Playwright.DotNet.PageObjects.Basket;
using Playwright.DotNet.PageObjects.Basket.CheckoutPage;
using Playwright.DotNet.PageObjects.Basket.SuccessPage;
using Playwright.DotNet.PageObjects.HomePage;
using Playwright.DotNet.PageObjects.Sections;
using Playwright.Dotnet.PageObjects.LoginPage;
using Playwright.DotNet.Services;
using Playwright.DotNet.Services.Contracts;
using Autofac;

namespace Playwright.DotNet.DI;

public static class DISetup
{
    public static void RegisterPageObjects(ContainerBuilder builder)
    {
        builder.RegisterType<LoginPage>().As<ILoginPage>();
        builder.RegisterType<HomePage>().As<IHomePage>();
        builder.RegisterType<BasketPage>().As<IBasketPage>();
        builder.RegisterType<CheckoutPage>().As<ICheckoutPage>();
        builder.RegisterType<SuccessPage>().As<ISuccessPage>();
        builder.RegisterType<HeaderSection>().As<IHeaderSection>();
    }

    public static void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterType<NavigationService>().As<INavigationService>();
        builder.RegisterType<ComponentCreateService>().As<IComponentCreateService>();
        builder.RegisterType<BrowserService>().As<IBrowserService>();
        builder.RegisterType<JavaScriptService>().As<IJavaScriptService>();
        builder.RegisterType<ComponentWaitService>().As<IComponentWaitService>();
    }

}