using Playwright.DotNet.Configuration;
using Playwright.DotNet.Infra;
using Playwright.DotNet.Services;
using Autofac;
using Playwright.DotNet.DI;
using Playwright.DotNet.Fixtures;

namespace EShopOnWeb.TestContainersSystemTests;

public class BaseTest
{
    protected App _app;
    protected TestExecutionEngine _testExecutionEngine;
    protected EShopOnWebApp EShopOnWebApp;
    protected ContainerBuilder _builder;
    protected IContainer _container;
    public SystemTestContainersFixture _fixture;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _fixture = new SystemTestContainersFixture();

    }

    [SetUp]
    public async Task SetUp()
    {
        await _fixture.SqlEdgeFixture.InitializeAsync();

        _app = new App();

        _builder = new ContainerBuilder();

        var config = new BrowserConfiguration();
        _builder.RegisterInstance(config).As<BrowserConfiguration>().SingleInstance();
        _testExecutionEngine = new TestExecutionEngine();
        _builder.RegisterInstance(_testExecutionEngine).AsSelf();
        _testExecutionEngine.StartBrowser(config, _builder);

        DISetup.RegisterPageObjects(_builder);
        DISetup.RegisterServices(_builder);

        _container = _builder.Build();
        ServiceLocator.SetContainer(_container);

        EShopOnWebApp = new EShopOnWebApp(_container);
        _app.Navigation.Navigate(_fixture.ServerAddress);
    }

    [TearDown]
    public async Task TearDown()
    {
        _testExecutionEngine.Dispose(_container);
        _app.Dispose();
        _container.Dispose();
        await _fixture.SqlEdgeFixture.StopContainer();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _fixture.SqlEdgeFixture.DisposeAsync();
        _fixture.Dispose();
    }
}
