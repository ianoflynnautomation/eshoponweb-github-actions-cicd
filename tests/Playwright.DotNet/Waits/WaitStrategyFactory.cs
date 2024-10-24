
namespace Playwright.DotNet.Waits;

/// <summary>
/// Factory for creating wait strategies.
/// </summary>
internal class WaitStrategyFactory
{
    internal static WaitToExistStrategy Exists(int? timeoutInterval = null, int? sleepinterval = null)
        => new(timeoutInterval, sleepinterval);

    internal static WaitToBeVisibleStrategy BeVisible(int? timeoutInterval = null, int? sleepinterval = null)
        => new(timeoutInterval, sleepinterval);

    internal static WaitToBeClickableStrategy BeClickable(int? timeoutInterval = null, int? sleepinterval = null)
        => new(timeoutInterval, sleepinterval);

}