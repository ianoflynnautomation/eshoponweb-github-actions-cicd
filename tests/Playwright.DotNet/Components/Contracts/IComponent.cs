
using Playwright.DotNet.SyncPlaywright.Core.Elements;

namespace Playwright.DotNet.Components.Contracts;

/// <summary>
/// Represents the component on the page.
/// </summary>
public interface IComponent
{   
    /// <summary>
    /// Gets the component name.
    /// </summary>
    string ComponentName { get; }
    /// <summary>
    /// Gets the page name.
    /// </summary>
    string PageName { get; }
    /// <summary>
    /// Gets the wrapped element.
    /// </summary>
    WebElement WrappedElement { get; }
    /// <summary>
    /// Gets the component type.
    /// </summary>
    Type ComponentType { get; }
    /// <summary>
    /// Gets the locator type.
    /// </summary>
    Type LocatorType { get; }
    /// <summary>
    /// Gets the locator value.
    /// </summary>
    string LocatorValue { get; }
}