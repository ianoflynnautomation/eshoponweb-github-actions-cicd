namespace Playwright.DotNet.Configuration.Options;

/// <summary>
/// Options for configuring the web settings.
/// </summary>
public class WebSettingsOptions
{
    public const string SectionName = "WebSettings";
    public bool AddUrlToBddLogging { get; set; }
    public bool ShouldAutomaticallyScrollToVisible { get; set; }
    public bool WaitUntilReadyOnElementFound { get; set; }
    public bool ShouldWaitForAngular { get; set; }
    public bool ShouldHighlightElements { get; set; }
    public bool FullPageScreenshotsEnabled { get; set; }
    public bool ShouldCaptureHttpTraffic { get; set; }
    public bool ShouldDisableJavaScript { get; set; }
    public string PathToSslCertificate { get; set; }
    public bool IsParallelExecutionEnabled { get; set; }
    public bool ShouldCheckForJavaScriptErrors { get; set; }
    public TimeoutOptions TimeoutSettings { get; set; }
    public ExecutionOptions ExecutionSettings { get; set; }
    public PlaywrightOptions PlaywrightSettings { get; set; }
}