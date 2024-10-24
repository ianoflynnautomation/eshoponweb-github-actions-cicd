namespace Playwright.DotNet.Configuration.Options;

public class TimeoutOptions
{
    public int PageLoadTimeout { get; set; } = 30;
    public int ScriptTimeout { get; set; } = 30;
    public int WaitUntilReadyTimeout { get; set; } = 30;
    public int WaitForJavaScriptAnimationsTimeout { get; set; } = 30;
    public int WaitForAngularTimeout { get; set; } = 30;
    public int WaitForPartialUrl { get; set; } = 30;
    public int ValidationsTimeout { get; set; } = 30;
    public int WaitForAjaxTimeout { get; set; } = 30;
    public int SleepInterval { get; set; } = 1;
    public int ElementToBeVisibleTimeout { get; set; } = 30;
    public int ElementToExistTimeout { get; set; } = 30;
    public int ElementToNotExistTimeout { get; set; } = 10;
    public int ElementToBeClickableTimeout { get; set; } = 30;
    public int ElementNotToBeVisibleTimeout { get; set; } = 10;
    public int ElementToHaveContentTimeout { get; set; } = 30;
    public int ActionTimeoutWhenHandlingDialogs { get; set; } = 10;
}