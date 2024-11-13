using Playwright.DotNet.Configuration;
using Playwright.DotNet.Infra;
using Playwright.DotNet.Services;
using Autofac;
using Playwright.DotNet.DI;
using Playwright.DotNet.Fixtures;
using Playwright.DotNet.Enums;

namespace EShopOnWeb.TestContainersSystemTests;

public class BaseTest
{
    protected SystemTestContainersFixture _fixture;
    protected App _app;
    protected TestExecutionEngine _testExecutionEngine;
    protected EShopOnWebApp _eShopOnWebApp;
    protected ContainerBuilder _builder;
    protected IContainer _container;

    protected TestContext TestContext => TestContext.CurrentContext;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _fixture = new SystemTestContainersFixture();
    }

    [SetUp]
    public async Task SetUp()
    {
        await _fixture.SqlEdgeFixture.InitializeAsync();

        _builder = new ContainerBuilder();
        var config = new BrowserConfiguration()
        {
            BrowserType = BrowserTypes.ChromiumHeadless,
        };

        _builder.RegisterInstance(config).As<BrowserConfiguration>().SingleInstance();
        _testExecutionEngine = new TestExecutionEngine();
        _builder.RegisterInstance(_testExecutionEngine).AsSelf();

        _testExecutionEngine.StartBrowser(config, _builder);
        
        DISetup.RegisterPageObjects(_builder);
        DISetup.RegisterServices(_builder);
        DISetup.RegisterApp(_builder);

        _container = _builder.Build();
        ServiceLocator.SetContainer(_container);

        _eShopOnWebApp = new EShopOnWebApp(_container);
    
        _app = ServiceLocator.Resolve<App>();
        await _app.Navigation.Navigate(_fixture.ServerAddress);
    }

    [TearDown]
    public async Task TearDown()
    {
        _testExecutionEngine.Dispose(_container);
        _app = ServiceLocator.Resolve<App>();
        _app.Dispose();
        _container.Dispose();
        //await _fixture.SqlEdgeFixture.StopContainer();
        await _fixture.SqlEdgeFixture.DisposeAsync();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        _fixture.Dispose();
    }
}
