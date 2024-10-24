using Playwright.DotNet.Components;
using Playwright.DotNet.Find;
using Playwright.DotNet.Services.Contracts;

namespace Playwright.DotNet.Services;

public class ComponentCreateService: IComponentCreateService
{
    public TComponent Create<TComponent, TBy>(TBy by)
        where TBy : FindStrategy
        where TComponent : Component
    {
        return ComponentRepository.CreateComponent<TComponent>(by);
    }

    public ComponentsList<TComponent> CreateAll<TComponent, TBy>(TBy by)
        where TBy : FindStrategy
        where TComponent : Component
    {
        return new ComponentsList<TComponent>(by);
    }
}
