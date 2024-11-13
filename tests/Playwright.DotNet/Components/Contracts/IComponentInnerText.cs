
namespace Playwright.DotNet.Components.Contracts;

public interface IComponentInnerText
{
    Task<string> InnerText { get; }
}