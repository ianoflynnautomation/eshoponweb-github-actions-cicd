using Autofac;

namespace Playwright.DotNet.DI;

public static class ServiceLocator
{
    private static IContainer _container;

     private static readonly object lockObject = new object();

    public static void SetContainer(IContainer container)
    {
        _container = container;
    }

    public static T Resolve<T>()
    {
        lock (lockObject) 
        if (_container is null)
        {
            throw new InvalidOperationException("Service locator is not initialized.");
        }
        
        return _container.Resolve<T>();
    }

    public static IContainer BuildContainer(ContainerBuilder builder)
    {
        return builder.Build();
    }


}