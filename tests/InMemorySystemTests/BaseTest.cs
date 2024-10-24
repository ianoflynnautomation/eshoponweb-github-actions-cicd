using Playwright.DotNet.Configuration;
using Playwright.DotNet.Infra;
using Playwright.DotNet.Services;
using Autofac;
using EShopOnWeb.InMemorySystemTests.Fixtures;
using Playwright.DotNet.DI;

namespace EShopOnWeb.InMemorySystemTests;

public class BaseTest
{
    protected ServerFixture _fixture;
    protected App _app;
    protected TestExecutionEngine _testExecutionEngine;
    protected EShopOnWebApp EShopOnWebApp;
    protected ContainerBuilder _builder; 
    protected IContainer _container;

    public TestContext TestContext => TestContext.CurrentContext;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _fixture = new ServerFixture();
    }

    [SetUp]
    public void SetUp()
    {  
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
    public void TearDown()
    {
        _testExecutionEngine.Dispose(_container);
        _app.Dispose();
        _container.Dispose();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _fixture.Dispose();
    }
}
