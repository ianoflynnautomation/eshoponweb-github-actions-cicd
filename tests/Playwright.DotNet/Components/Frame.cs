using Playwright.DotNet.Playwright.Core.Elements;

namespace Playwright.DotNet.Components;

public class Frame : Component
{
    public Task<string> Name => GetAttributeAsync("name");

    public override TComponent As<TComponent>()
    {
        var component = Activator.CreateInstance<TComponent>();
        component.By = By;

        if (component is not Frame)
        {
            component.WrappedElement = new WebElement(WrappedElement.Page, WrappedElement.WrappedLocator);
        }
        else
        {
            component.WrappedElement = WrappedElement;
        }

        return component;
    }

}