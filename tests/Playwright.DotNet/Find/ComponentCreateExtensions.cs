
using Playwright.DotNet.Components;

namespace Playwright.DotNet.Find;

public static class ComponentCreateExtensions
{

    public static TComponent CreateByXpath<TComponent>(this Component component, string xpath, bool shouldCacheElement = false)
        where TComponent : Component => component.Create<TComponent, FindXPathStrategy>(new FindXPathStrategy(xpath), shouldCacheElement);

    public static TComponent CreateByTag<TComponent>(this Component component, string tag, bool shouldCacheElement = false)
        where TComponent : Component => component.Create<TComponent, FindTagStrategy>(new FindTagStrategy(tag), shouldCacheElement);

    public static TComponent CreateByCss<TComponent>(this Component component, string cssClass, bool shouldCacheElement = false)
        where TComponent : Component => component.Create<TComponent, FindStrategy>(new FindCssStrategy(cssClass), shouldCacheElement);

    public static TComponent CreateByClassContaining<TComponent>(this Component component, string cssClassContaining, bool shouldCacheElement = false)
        where TComponent : Component => component.Create<TComponent, FindClassContainingStrategy>(new FindClassContainingStrategy(cssClassContaining), shouldCacheElement);

    public static TComponent CreateByDataTestId<TComponent>(this Component component, string dataTestId)
        where TComponent : Component => component.Create<TComponent, FindDataTestIdStrategy>(new FindDataTestIdStrategy(dataTestId));

    public static ComponentsList<TComponent> CreateAllByXpath<TComponent>(this Component component, string xpath)
        where TComponent : Component => new ComponentsList<TComponent>(new FindXPathStrategy(xpath), component);

    public static ComponentsList<TComponent> CreateAllByCss<TComponent>(this Component component, string cssClass)
        where TComponent : Component => new ComponentsList<TComponent>(new FindCssStrategy(cssClass), component);

    public static ComponentsList<TComponent> CreateAllByClassContaining<TComponent>(this Component component, string cssClassContaining)
        where TComponent : Component => new ComponentsList<TComponent>(new FindClassContainingStrategy(cssClassContaining), component);


}