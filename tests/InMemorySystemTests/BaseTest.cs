using Playwright.DotNet.Configuration;
using Playwright.DotNet.Infra;
using Playwright.DotNet.Services;
using Autofac;
using Playwright.DotNet.DI;
using Playwright.DotNet.Fixtures;
using Playwright.DotNet.Enums;

namespace EShopOnWeb.InMemorySystemTests;

public class BaseTest
{
    protected SystemTestFixture _fixture;
    protected App _app;
    protected TestExecutionEngine _testExecutionEngine;
    protected EShopOnWebApp _eShopOnWebApp;
    protected ContainerBuilder _builder; 
    protected IContainer _container;
    protected TestContext TestContext => TestContext.CurrentContext;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _fixture = new SystemTestFixture();
    }

    [SetUp]
    public void SetUp()
    {  
        _builder = new ContainerBuilder();
        var config = new BrowserConfiguration()
        {
            BrowserType = BrowserTypes.ChromiumHeadless,
        };


        _builder.RegisterInstance(config).As<BrowserConfiguration>().SingleInstance();
        _testExecutionEngine = new TestExecutionEngine();
        _builder.RegisterInstance(_testExecutionEngine).AsSelf();

         _fixture.Logger.Information($"Start test {TestContext.Test.FullName}");
        _testExecutionEngine.StartBrowser(config, _builder);
        
        DISetup.RegisterPageObjects(_builder);
        DISetup.RegisterServices(_builder);
        DISetup.RegisterApp(_builder);

        _container = _builder.Build();
        ServiceLocator.SetContainer(_container);

        _eShopOnWebApp = new EShopOnWebApp(_container);
        _fixture.Logger.Information("Navigating to the web server url {WebServerUrl}", _fixture.SystemTestHost.WebServerUrl);

        _app = ServiceLocator.Resolve<App>();
        _app.Navigation.Navigate(_fixture.SystemTestHost.WebServerUrl);
    }

    [TearDown]
    public void TearDown()
    {
        _fixture.Logger.Information($"End test {TestContext.Test.FullName}");
        _testExecutionEngine.Dispose(_container);
        _app = ServiceLocator.Resolve<App>();
        _app.Dispose();
        _container.Dispose();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _fixture.Dispose();
    }
}
