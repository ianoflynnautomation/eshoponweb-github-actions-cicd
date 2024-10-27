
using System.Diagnostics;
using System.Reflection;
using Playwright.DotNet.Components;
using Playwright.DotNet.DI;
using Playwright.DotNet.Find;
using Playwright.DotNet.SyncPlaywright.Core.Elements;

namespace Playwright.DotNet.Services;
public static class ComponentRepository
{
    private static WrappedBrowser WrappedBrowser => ServiceLocator.Resolve<WrappedBrowser>();

    public static dynamic CreateComponentWithParent(FindStrategy by, Component parenTComponent, Type newElementType)
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        dynamic? element = Activator.CreateInstance(newElementType);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        element.By = by;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        element.ParentComponent = parenTComponent;
        ResolveRelativeWebElement(element);
        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = pageName ?? string.Empty;

        return element;
    }

    public static TComponentType CreateComponentWithParent<TComponentType>(FindStrategy by, Component parenTComponent)
        where TComponentType : Component
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        var element = Activator.CreateInstance<TComponentType>();

        element.By = by;
        element.ParentComponent = parenTComponent;
        ResolveRelativeWebElement(element);
        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({element.By})" : elementName;
        element.PageName = pageName ?? string.Empty;

        return element;
    }

    public static TComponentType CreateComponentWithParent<TComponentType>(FindStrategy by, Component parenTComponent, WebElement element)
    where TComponentType : Component
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        var component = Activator.CreateInstance<TComponentType>();

        component.WrappedElement = element;
        component.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({component.By})" : elementName;
        component.PageName = pageName ?? string.Empty;
        component.ParentComponent = parenTComponent;

        return component;
    }

    public static ComponentsList<TComponentType> CreateComponentListWithParent<TComponentType>(FindStrategy by, Component parenTComponent)
        where TComponentType : Component
    {
        var list = new List<TComponentType>();

        var webElements = by.Resolve(parenTComponent.WrappedElement).All();
        foreach (var element in webElements)
        {
            list.Add(CreateComponentWithParent<TComponentType>(by, parenTComponent, element));
        }

        return new ComponentsList<TComponentType>(list);
    }

    public static ComponentsList<TComponentType> CreateComponentList<TComponentType>(FindStrategy by)
        where TComponentType : Component
    {
        var list = new List<TComponentType>();
        var webElements = by.Resolve(WrappedBrowser.CurrentPage).All();
        foreach (var element in webElements)
        {
            list.Add(CreateComponent<TComponentType>(by));
        }

        return new ComponentsList<TComponentType>(list);
    }


    public static TComponentType CreateComponent<TComponentType>(FindStrategy by)
        where TComponentType : Component
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        var element = Activator.CreateInstance<TComponentType>();
        element.By = by;

        ResolveWebElement(element);

        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = pageName ?? string.Empty;

        return element;
    }

    private static void DetermineComponentAttributes(out string elementName, out string pageName)
    {
        elementName = string.Empty;
        pageName = string.Empty;
        try
        {
            var callStackTrace = new StackTrace();
            var currentAssembly = typeof(ComponentRepository).Assembly;

            foreach (var frame in callStackTrace.GetFrames())
            {
                var frameMethodInfo = frame.GetMethod() as MethodInfo;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (!frameMethodInfo?.ReflectedType?.Assembly.Equals(currentAssembly) == true &&
                    !frameMethodInfo.IsStatic &&
                    frameMethodInfo.ReturnType.IsSubclassOf(typeof(Component)))
                {
                    elementName = frame.GetMethod().Name.Replace("get_", string.Empty);

                    break;
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
        }
        catch (Exception ex)
        {
             Debug.WriteLine(ex);
            
        }
    }

    private static void ResolveWebElement(Component component)
    {
        if (component is Frame)
        {
            component.WrappedElement = new FrameElement(WrappedBrowser.CurrentPage, component.By.Resolve(WrappedBrowser.CurrentPage));
        }
        else
        {
            component.WrappedElement = component.By.Resolve(WrappedBrowser.CurrentPage);
        }
    }

    private static void ResolveRelativeWebElement(Component component)
    {
        if (component is Frame)
        {
            component.WrappedElement = new FrameElement(WrappedBrowser.CurrentPage, component.By.Resolve(component.ParentComponent.WrappedElement));
        }
        else
        {
            component.WrappedElement = component.By.Resolve(component.ParentComponent.WrappedElement);
        }
    }
}
