
namespace Playwright.DotNet.Configuration.Options;

/// <summary>
/// Options for configuring the web settings.
/// </summary>
public class WebSettingsOptions
{
    public const string SectionName = "WebSettings";
    public TimeoutOptions? TimeoutSettings { get; set; }
    public PlaywrightOptions? PlaywrightSettings { get; set; }
}