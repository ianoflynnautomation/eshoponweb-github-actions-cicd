
namespace Playwright.DotNet.Components.Contracts;

/// <summary>
/// represents a component that has text
/// </summary>
public interface IComponentText
{
    /// <summary>
    /// Gets the text of the component
    /// </summary>
    Task<string> GetTextAsync();
}