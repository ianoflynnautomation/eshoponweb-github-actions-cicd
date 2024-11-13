using Playwright.DotNet.Components;

namespace Playwright.DotNet.Waits;

/// <summary>
/// Extensions for wait strategies.
/// </summary>
public static class WaitStrategyComponentsExtensions
{
    public static TComponentType ToExists<TComponentType>(this TComponentType element, int? timeoutInterval = default)
        where TComponentType : Component
    {
        var until = new WaitToExistStrategy(timeoutInterval);
        element.EnsureState(until);
        return element;
    }

    public static TComponentType ToBeVisible<TComponentType>(this TComponentType element, int? timeoutInterval = default)
      where TComponentType : Component
    {
        var until = new WaitToBeVisibleStrategy(timeoutInterval);
        element.EnsureState(until);
        return element;
    }

    public static TComponentType ToBeClickable<TComponentType>(this TComponentType element, int? timeoutInterval = default)
     where TComponentType : Component
    {
        var until = new WaitToBeClickableStrategy(timeoutInterval);
        element.EnsureState(until);
        return element;
    }
}
