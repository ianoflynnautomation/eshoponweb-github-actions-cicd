using Playwright.DotNet.Components;
using Playwright.DotNet.Services;
using Playwright.DotNet.Services.Contracts;

namespace Playwright.DotNet.Find;
public static class ComponentRepositoryExtensions
{

    public static TComponent CreateByXpath<TComponent>(this IComponentCreateService repository, string xpath)
        where TComponent : Component => repository.Create<TComponent, FindXPathStrategy>(new FindXPathStrategy(xpath));

    public static TComponent CreateByTag<TComponent>(this IComponentCreateService repository, string tag)
        where TComponent : Component => repository.Create<TComponent, FindTagStrategy>(new FindTagStrategy(tag));

    public static TComponent CreateByCss<TComponent>(this IComponentCreateService repository, string cssClass)
        where TComponent : Component => repository.Create<TComponent, FindCssStrategy>(new FindCssStrategy(cssClass));

    public static TComponent CreateByClassContaining<TComponent>(this IComponentCreateService repository, string cssClassContaining)
        where TComponent : Component => repository.Create<TComponent, FindClassContainingStrategy>(new FindClassContainingStrategy(cssClassContaining));

    public static TComponent CreateByDataTestId<TComponent>(this IComponentCreateService repository, string dataTestId)
        where TComponent : Component => repository.Create<TComponent, FindDataTestIdStrategy>(new FindDataTestIdStrategy(dataTestId));

    public static ComponentsList<TComponent> CreateAllByXpath<TComponent>(this IComponentCreateService repository, string xpath)
        where TComponent : Component => new ComponentsList<TComponent>(new FindXPathStrategy(xpath));

    public static ComponentsList<TComponent> CreateAllByCss<TComponent>(this IComponentCreateService repository, string cssClass)
        where TComponent : Component => new ComponentsList<TComponent>(new FindCssStrategy(cssClass));

    public static ComponentsList<TComponent> CreateAllByClassContaining<TComponent>(this IComponentCreateService repository, string classContaining)
        where TComponent : Component => new ComponentsList<TComponent>(new FindClassContainingStrategy(classContaining));
}