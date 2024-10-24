using Playwright.DotNet.SyncPlaywright.Core.Elements;

namespace Playwright.DotNet.Components;

public class Frame : Component
{
    public string Name => GetAttribute("name");

    public override TComponent As<TComponent>()
    {
        var component = Activator.CreateInstance<TComponent>();
        component.By = this.By;

        if (component is not Frame)
        {
            component.WrappedElement = new WebElement(this.WrappedElement.Page, this.WrappedElement.WrappedLocator);
        }
        else
        {
            component.WrappedElement = this.WrappedElement;
        }

        return component;
    }

}