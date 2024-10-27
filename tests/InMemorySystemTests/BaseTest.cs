using Playwright.DotNet.Configuration;
using Playwright.DotNet.Infra;
using Playwright.DotNet.Services;
using Autofac;
using Playwright.DotNet.DI;
using Microsoft.Extensions.Logging;
using Playwright.DotNet.Fixtures;
using Playwright.DotNet.Enums;

namespace EShopOnWeb.InMemorySystemTests;

public class BaseTest
{
    protected SystemTestFixture _fixture;
    protected App _app;
    protected TestExecutionEngine _testExecutionEngine;
    protected EShopOnWebApp EShopOnWebApp;
    protected ContainerBuilder _builder; 
    protected IContainer _container;

    public TestContext TestContext => TestContext.CurrentContext;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _fixture = new SystemTestFixture();
    }

    [SetUp]
    public void SetUp()
    {  
        _builder = new ContainerBuilder();
        //_fixture.Logger.LogInformation("Setting up test");
        var config = new BrowserConfiguration()
        {
            BrowserType = BrowserTypes.ChromiumHeadless,
        };


        _builder.RegisterInstance(config).As<BrowserConfiguration>().SingleInstance();
        _testExecutionEngine = new TestExecutionEngine();
        _builder.RegisterInstance(_testExecutionEngine).AsSelf();
         //_fixture.Logger.LogInformation("Starting browser");
        _testExecutionEngine.StartBrowser(config, _builder);
        
        DISetup.RegisterPageObjects(_builder);
        DISetup.RegisterServices(_builder);
        DISetup.AddApp(_builder);

        _container = _builder.Build();
        ServiceLocator.SetContainer(_container);

        EShopOnWebApp = new EShopOnWebApp(_container);
          //_fixture.Logger.LogInformation("Navigating to server address");

        _app = ServiceLocator.Resolve<App>();
        _app.Navigation.Navigate(_fixture.SystemTestHost.WebServerUrl);
    }

    [TearDown]
    public void TearDown()
    {
        //_fixture.Logger.LogInformation("Tearing down test");
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
