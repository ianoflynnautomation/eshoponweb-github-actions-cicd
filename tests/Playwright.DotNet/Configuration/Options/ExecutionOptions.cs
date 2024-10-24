

namespace Playwright.DotNet.Configuration.Options;
public class ExecutionOptions
{
    public string ExecutionType { get; set; }
    public string DefaultBrowser { get; set; }
    public string BrowserVersion { get; set; }
    public string DefaultLifeCycle { get; set; }
    public string Resolution { get; set; }
    public string GridUrl { get; set; }
    public string FileRemoteLocation { get; set; }
    public List<Capabilities> Arguments { get; set; }
    public string PackedExtensionPath { get; set; }
    public string UnpackedExtensionPath { get; set; }
    public bool IsCloudRun { get; set; }
}