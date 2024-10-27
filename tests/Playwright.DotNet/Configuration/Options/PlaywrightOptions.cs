using Microsoft.Playwright;

namespace Playwright.DotNet.Configuration.Options;

public sealed class PlaywrightOptions
{
    public BrowserNewContextOptions? ContextOptions { get; set; }
}
